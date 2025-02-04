using Core.Domain.Dtos.Auth;
using MediatR;

namespace Core.Domain.Commands.User;

/// <summary>
/// Command to create a new user.
/// </summary>
public record SignUpCommand(
    string Username,
    string? FullName,
    string Email,
    string Password) : IRequest<SignUpResponse>;