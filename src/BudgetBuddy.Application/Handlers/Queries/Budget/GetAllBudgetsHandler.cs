using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Queries.Budget;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Queries.Budget;

public class GetAllBudgetsHandler(IBudgetRepository budgetRepository, ITokenHelper tokenHelper) : IRequestHandler<GetAllBudgetsQuery, List<Domain.Budgets.Budget>>
{
    public async Task<List<Domain.Budgets.Budget>> Handle(GetAllBudgetsQuery request, CancellationToken cancellationToken)
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