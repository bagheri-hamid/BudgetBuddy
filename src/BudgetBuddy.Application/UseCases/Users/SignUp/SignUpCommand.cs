using BudgetBuddy.Domain.Dtos.Auth;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Users.SignUp;

/// <summary>
/// Command to create a new user.
/// </summary>
public record SignUpCommand(
    string Username,
    string? FullName,
    string Email,
    string Password) : IRequest<SignUpResponse>;