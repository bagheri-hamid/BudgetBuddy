using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Queries.Transaction;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Queries.Transaction;

public class GetAllTransactionsHandler(ITransactionRepository transactionRepository, ITokenHelper tokenHelper)
    : IRequestHandler<GetAllTransactionsQuery, List<Domain.Entities.Transaction>>
{
    public async Task<List<Domain.Entities.Transaction>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await transactionRepository.FindAsync(
            t =>
                t.UserId == tokenHelper.GetUserId() &&
                (string.IsNullOrWhiteSpace(request.Description) || (t.Description != null && t.Description.Contains(request.Description))) &&
                (request.StartTime == null || t.Date >= request.StartTime) &&
                (request.EndTime == null || t.Date >= request.EndTime) &&
                (request.CategoryId == null || t.CategoryId == request.CategoryId) &&
                (request.AccountId == null || t.AccountId == request.AccountId)
            ,
            request.Offset,
            request.Limit,
            cancellationToken
        );

        return transactions;
    }
}