using Core.Application.Interfaces;
using Core.Domain.Dtos.Auth;
using Core.Domain.Entities;
using Core.Domain.Exceptions;

namespace Core.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IEmailValidator emailValidator,
    IPasswordHasher passwordHasher,
    IJwtService jwtService
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
            throw new InvalidUsernameInSignUpException();

        if (!emailValidator.IsValid(email))
            throw new InvalidEmailFormatException();

        // Check existing users
        var existingUser = await userRepository.FindOneAsync(user => user.Username == username);
        if (existingUser is not null)
            throw new UsernameHasAlreadyTakenException();

        var existingEmail = await userRepository.FindOneAsync(user => user.Email == email);
        if (existingEmail is not null)
            throw new EmailHasBeenRegisteredException();

        // Password requirements
        if (password.Length < 8)
            throw new InvalidPasswordLengthException(8);

        // Hash password
        var passwordHash = passwordHasher.Hash(password);

        // Create user entity
        var user = new User
        {
            Username = username.Trim(),
            FullName = fullName?.Trim(),
            Email = email.ToLowerInvariant(),
            Password = passwordHash,
        };

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

        var token = jwtService.GenerateToken(user);
        
        return new SignUpResponse(user.Id, token);
    }

    public async Task<LoginResponse> Login(string username, string password)
    {
        var user = await userRepository.FindOneAsync(user => user.Username == username);

        if (user == null || !passwordHasher.Verify(user.Password, password))
            throw new InvalidUsernameOrPasswordException();

        var token = jwtService.GenerateToken(user);

        return new LoginResponse(token);
    }
}