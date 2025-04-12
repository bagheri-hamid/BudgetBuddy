using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Commands.Budget;
using BudgetBuddy.Domain.Exceptions;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Commands.Budget;

public class DeleteBudgetHandler(IBudgetRepository budgetRepository, ITokenHelper tokenHelper) : IRequestHandler<DeleteBudgetCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository.FindOneAsync(b => b.Id == request.Id && b.UserId == tokenHelper.GetUserId());

        if (budget == null)
            throw new ObjectNotFoundException("Budget");
        
        budget.IsDeleted = true;

        await budgetRepository.SaveChangesAsync();
        
        return Unit.Value;
    }
}