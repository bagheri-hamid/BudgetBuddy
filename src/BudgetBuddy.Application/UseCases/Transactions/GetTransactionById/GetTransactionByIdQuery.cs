using BudgetBuddy.Domain.Transactions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.GetTransactionById;

public record GetTransactionByIdQuery(Guid Id) : IRequest<Transaction>;