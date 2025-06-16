using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.ValueObjects;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Budgets.CreateBudget;

public class CreateBudgetHandler(
    IBudgetRepository budgetRepository,
    ITokenHelper tokenHelper,
    ICategoryRepository categoryRepository
) : IRequestHandler<CreateBudgetCommand, Budget>
{
    public async Task<Budget> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount < 1)
            throw new CanNotBeLessThanZeroException();

        if (request.StartDate == default || request.EndDate == default)
            throw new InvalidDateException();
        
        if (!await categoryRepository.IsExistsAsync(c => c.Id == request.CategoryId, cancellationToken))
            throw new ObjectNotFoundException("Category");

        var budget = new Budget(new Money(request.Amount), request.Description, request.StartDate, request.EndDate, request.CategoryId, tokenHelper.GetUserId());
        await budgetRepository.AddAsync(budget, cancellationToken);
        await budgetRepository.SaveChangesAsync(cancellationToken);

        return budget;
    }
}