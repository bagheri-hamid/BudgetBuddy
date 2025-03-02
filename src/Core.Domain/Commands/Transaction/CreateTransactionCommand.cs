using Core.Domain.Enums;
using MediatR;

namespace Core.Domain.Commands.Transaction;

/// <summary>
/// Command for creating a new transaction
/// </summary>
public record CreateTransactionCommand(long Amount, string Description, TransactionType Type, DateTime Date, Guid CategoryId, Guid AccountId) : IRequest<Entities.Transaction>;