using BudgetBuddy.Domain.Dtos.Auth;
using MediatR;

namespace BudgetBuddy.Domain.Commands.User;

/// <summary>
/// Command to create a new user.
/// </summary>
public record SignUpCommand(
    string Username,
    string? FullName,
    string Email,
    string Password) : IRequest<SignUpResponse>;