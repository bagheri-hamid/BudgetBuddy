using BudgetBuddy.Domain.Enums;
using MediatR;

namespace BudgetBuddy.Domain.Commands.Transaction;

/// <summary>
/// Command for updating trans
/// </summary>
public record UpdateTransactionCommand(Guid TransActionId, long Amount, string Description, TransactionType Type, DateTime Date, Guid CategoryId)
    : IRequest<Entities.Transaction>;