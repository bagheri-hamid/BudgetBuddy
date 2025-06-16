using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.ValueObjects;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Budgets.UpdateBudget;

public class UpdateBudgetHandler(
    IBudgetRepository budgetRepository,
    ITokenHelper tokenHelper,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateBudgetCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount < 1)
            throw new CanNotBeLessThanZeroException();

        if (request.StartDate == default || request.EndDate == default)
            throw new InvalidDateException();

        var budget = await budgetRepository.FindOneAsync(b => b.Id == request.Id && b.UserId == tokenHelper.GetUserId(), cancellationToken);

        if (budget == null)
            throw new ObjectNotFoundException("Budget");

        budget.Update(new Money(request.Amount), request.Description, request.StartDate, request.EndDate);

        await unitOfWork.CompleteAsync(cancellationToken);

        return Unit.Value;
    }
}