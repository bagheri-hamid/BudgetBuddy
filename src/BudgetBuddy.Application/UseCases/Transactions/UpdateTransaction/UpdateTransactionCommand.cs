using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Transactions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.UpdateTransaction;

/// <summary>
/// Command for updating trans
/// </summary>
public record UpdateTransactionCommand(Guid TransActionId, long Amount, string Description, TransactionType Type, DateTime Date, Guid CategoryId)
    : IRequest<Transaction>;