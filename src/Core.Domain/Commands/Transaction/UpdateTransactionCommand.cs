using Core.Domain.Enums;
using MediatR;

namespace Core.Domain.Commands.Transaction;

/// <summary>
/// Command for updating trans
/// </summary>
public record UpdateTransactionCommand(Guid TransActionId, long Amount, string Description, TransactionType Type, DateTime Date, Guid CategoryId)
    : IRequest<Entities.Transaction>;