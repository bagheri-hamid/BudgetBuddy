using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Exceptions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Categories.UpdateCategory;

public class UpdateCategoryHandler(ICategoryRepository categoryRepository, ITokenHelper tokenHelper) : IRequestHandler<UpdateCategoryCommand, Category>
{
    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FindOneAsync(c => c.Id == request.Id && c.UserId == tokenHelper.GetUserId(), cancellationToken);

        if (category == null)
            throw new ObjectNotFoundException("Category");
        
        category.Update(request.Name);
        await categoryRepository.SaveChangesAsync(cancellationToken);
        
        return category;
    }
}