using MediatR;

namespace Core.Domain.Commands.Account;

/// <summary>
/// Command for creating a new account
/// </summary>
public record CreateAccountCommand(string Name, string Type, long Balance) : IRequest<Entities.Account>;