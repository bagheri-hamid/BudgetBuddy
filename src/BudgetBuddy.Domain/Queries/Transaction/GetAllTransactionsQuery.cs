using MediatR;

namespace BudgetBuddy.Domain.Queries.Transaction;

public record GetAllTransactionsQuery(string? Description, DateTime? StartTime, DateTime? EndTime, Guid? CategoryId, Guid? AccountId, int Offset = 0, int Limit = 20)
    : IRequest<List<Transactions.Transaction>>;