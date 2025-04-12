using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Commands.Category;
using BudgetBuddy.Domain.Exceptions;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Commands.Category;

public class UpdateCategoryHandler(ICategoryRepository categoryRepository, ITokenHelper tokenHelper) : IRequestHandler<UpdateCategoryCommand, Domain.Entities.Category>
{
    public async Task<Domain.Entities.Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FindOneAsync(c => c.Id == request.Id && c.UserId == tokenHelper.GetUserId());

        if (category == null)
            throw new ObjectNotFoundException("Category");
        
        category.Name = request.Name;
        category.UpdatedAt = DateTime.UtcNow;
        
        await categoryRepository.SaveChangesAsync();
        
        return category;
    }
}