using Core.Application.Interfaces;
using Core.Domain.Exceptions;
using Core.Domain.Queries.Budget;
using MediatR;

namespace Core.Application.Handlers.Queries.Budget;

public class GetBudgetByIdHandler(IBudgetRepository budgetRepository, ITokenHelper tokenHelper) : IRequestHandler<GetBudgetByIdQuery, Domain.Entities.Budget>
{
    public async Task<Domain.Entities.Budget> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository.FindOneAsync(b => b.Id == request.Id && b.UserId == tokenHelper.GetUserId());

        if (budget == null)
            throw new ObjectNotFoundException("Budget");
        
        return budget;
    }
}