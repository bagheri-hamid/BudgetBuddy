using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Commands.Budget;
using BudgetBuddy.Domain.Exceptions;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Commands.Budget;

public class CreateBudgetHandler(
    IBudgetRepository budgetRepository,
    ITokenHelper tokenHelper,
    ICategoryRepository categoryRepository
) : IRequestHandler<CreateBudgetCommand, Domain.Entities.Budget>
{
    public async Task<Domain.Entities.Budget> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount < 1)
            throw new CanNotBeLessThanZeroException();

        if (request.StartDate == default || request.EndDate == default)
            throw new InvalidDateException();
        
        if (!await categoryRepository.IsExistsAsync(c => c.Id == request.CategoryId))
            throw new ObjectNotFoundException("Category");
        
        var budget = new Domain.Entities.Budget
        {
            Amount = request.Amount,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CategoryId = request.CategoryId,
            UserId = tokenHelper.GetUserId(),
        };
        
        await budgetRepository.AddAsync(budget);
        await budgetRepository.SaveChangesAsync();

        return budget;
    }
}