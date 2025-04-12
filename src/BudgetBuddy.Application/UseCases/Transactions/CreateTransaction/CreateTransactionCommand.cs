using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Transactions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.CreateTransaction;

/// <summary>
/// Command for creating a new transaction
/// </summary>
public record CreateTransactionCommand(long Amount, string Description, TransactionType Type, DateTime Date, Guid CategoryId, Guid AccountId) : IRequest<Transaction>;