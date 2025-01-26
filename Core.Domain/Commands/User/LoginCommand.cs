using Core.Domain.Responses.User;
using MediatR;

namespace Core.Domain.Commands.User;

/// <summary>
/// Command to log in.
/// </summary>
public record LoginCommand(
    string Username,
    string Password
) : IRequest<LoginResponse>;