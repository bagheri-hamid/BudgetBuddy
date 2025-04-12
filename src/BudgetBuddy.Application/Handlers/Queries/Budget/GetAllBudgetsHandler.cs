using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Queries.Budget;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Queries.Budget;

public class GetAllBudgetsHandler(IBudgetRepository budgetRepository, ITokenHelper tokenHelper) : IRequestHandler<GetAllBudgetsQuery, List<Domain.Entities.Budget>>
{
    public async Task<List<Domain.Entities.Budget>> Handle(GetAllBudgetsQuery request, CancellationToken cancellationToken)
    {
        var budgets = await budgetRepository.FindAsync(
            b =>
                b.UserId == tokenHelper.GetUserId() &&
                (request.Amount == null || b.Amount == request.Amount) &&
                (string.IsNullOrWhiteSpace(request.Description) || b.Description == request.Description),
            request.Offset,
            request.Limit
        );
        
        return budgets;
    }
}