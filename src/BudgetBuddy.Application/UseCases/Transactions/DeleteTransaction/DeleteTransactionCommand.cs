using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.DeleteTransaction;

public record DeleteTransactionCommand(Guid Id) : IRequest<Unit>;