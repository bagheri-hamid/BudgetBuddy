using BudgetBuddy.Domain.Transactions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.GetAllTransactions;

public record GetAllTransactionsQuery(string? Description, DateTime? StartTime, DateTime? EndTime, Guid? CategoryId, Guid? AccountId, int Offset = 0, int Limit = 20)
    : IRequest<List<Transaction>>;