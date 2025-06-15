using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Exceptions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Budgets.DeleteBudget;

public class DeleteBudgetHandler(IBudgetRepository budgetRepository, ITokenHelper tokenHelper) : IRequestHandler<DeleteBudgetCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository.FindOneAsync(b => b.Id == request.Id && b.UserId == tokenHelper.GetUserId(), cancellationToken);

        if (budget == null)
            throw new ObjectNotFoundException("Budget");
        
        budget.Delete();

        await budgetRepository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}