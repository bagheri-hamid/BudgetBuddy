using BudgetBuddy.Domain.Dtos.Auth;
using MediatR;

namespace BudgetBuddy.Domain.Commands.User;

/// <summary>
/// Command to log in.
/// </summary>
public record LoginCommand(
    string Username,
    string Password
) : IRequest<LoginResponse>;