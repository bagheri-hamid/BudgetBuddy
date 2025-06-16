using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Dtos.Auth;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Users;
using BudgetBuddy.Domain.ValueObjects;

namespace BudgetBuddy.Application.Services;

public class AuthService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService,
    IUnitOfWork  unitOfWork
) : IAuthService, IScopedDependency
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
        
        // Check existing users
        var existingUser = await userRepository.FindOneAsync(user => user.Username == username);
        if (existingUser is not null)
            throw new UsernameHasAlreadyTakenException();

        var emailValueObject = Email.Create(email);
        
        var existingEmail = await userRepository.FindOneAsync(user => user.Email == emailValueObject);
        if (existingEmail is not null)
            throw new EmailHasBeenRegisteredException();

        // Password requirements
        if (password.Length < 8)
            throw new InvalidPasswordLengthException(8);

        // Hash password
        var passwordHash = passwordHasher.Hash(password);

        // Create user entity
        var user = new User(username.Trim(), fullName?.Trim(), emailValueObject, passwordHash);

        await userRepository.AddAsync(user);
        await unitOfWork.CompleteAsync();

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