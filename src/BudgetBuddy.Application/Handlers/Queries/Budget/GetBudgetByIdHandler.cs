using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Queries.Budget;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Queries.Budget;

public class GetBudgetByIdHandler(IBudgetRepository budgetRepository, ITokenHelper tokenHelper) : IRequestHandler<GetBudgetByIdQuery, Domain.Budgets.Budget>
{
    public async Task<Domain.Budgets.Budget> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository.FindOneAsync(b => b.Id == request.Id && b.UserId == tokenHelper.GetUserId());

        if (budget == null)
            throw new ObjectNotFoundException("Budget");
        
        return budget;
    }
}