using MediatR;

namespace Core.Domain.Commands.Account;

public record DeleteAccountCommand(Guid AccountId) : IRequest<Unit>;