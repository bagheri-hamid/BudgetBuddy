using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Responses.User;

namespace Core.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IEmailValidator emailValidator,
    IPasswordHasher passwordHasher
) : IUserService, IScopedDependency
{
    public async Task<SignUpResponse> SignUp(
        string username,
        string? fullName,
        string email,
        string password)
    {
        // Input validation
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is required", nameof(username));

        if (!emailValidator.IsValid(email))
            throw new ArgumentException("Invalid email format", nameof(email));

        // Check existing users
        var existingUser = await userRepository.FindOneAsync(user => user.Username == username);
        if (existingUser is not null)
            throw new Exception("Username has already taken");

        var existingEmail = await userRepository.FindOneAsync(user => user.Email == email);
        if (existingEmail is not null)
            throw new Exception("Email has already registered!");

        // Password requirements
        if (password.Length < 8)
            throw new Exception("Password must be at least 8 characters");

        // Hash password
        var passwordHash = passwordHasher.Hash(password);

        // Create user entity
        var user = new User
        {
            Username = username.Trim(),
            FullName = fullName?.Trim(),
            Email = email.ToLowerInvariant(),
            Password = passwordHash,
            CreatedDate = DateTime.UtcNow,
            LastModifiedDate = null
        };

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

        return new SignUpResponse
        {
            IsSuccess = true,
            Message = null,
            UserId = user.Id,
            JwtToken = null,
            SessionId = null
        };
    }
}