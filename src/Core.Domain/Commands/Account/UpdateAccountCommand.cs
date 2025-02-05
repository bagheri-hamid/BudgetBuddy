using MediatR;

namespace Core.Domain.Commands.Account;

/// <summary>
/// Command for updating an account
/// </summary>
public record UpdateAccountCommand(Guid AccountId, string Name, string Type, long Balance) : IRequest<Entities.Account>;