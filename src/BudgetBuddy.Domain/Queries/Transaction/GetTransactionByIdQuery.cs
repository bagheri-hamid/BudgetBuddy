using MediatR;

namespace BudgetBuddy.Domain.Queries.Transaction;

public record GetTransactionByIdQuery(Guid Id) : IRequest<Transactions.Transaction>;