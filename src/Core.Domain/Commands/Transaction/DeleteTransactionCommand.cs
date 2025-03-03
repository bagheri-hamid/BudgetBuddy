using MediatR;

namespace Core.Domain.Commands.Transaction;

public record DeleteTransactionCommand(Guid Id) : IRequest<Unit>;