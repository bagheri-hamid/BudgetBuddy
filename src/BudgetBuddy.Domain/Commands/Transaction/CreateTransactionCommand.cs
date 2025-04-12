using BudgetBuddy.Domain.Enums;
using MediatR;

namespace BudgetBuddy.Domain.Commands.Transaction;

/// <summary>
/// Command for creating a new transaction
/// </summary>
public record CreateTransactionCommand(long Amount, string Description, TransactionType Type, DateTime Date, Guid CategoryId, Guid AccountId) : IRequest<Transactions.Transaction>;