using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Queries.Category;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Queries.Category;

public class GetCategoryByIdHandler(ICategoryRepository categoryRepository, ITokenHelper tokenHelper) : IRequestHandler<GetCategoryByIdQuery, Domain.Categories.Category>
{
    public async Task<Domain.Categories.Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FindOneAsync(c => c.Id == request.Id && c.UserId == tokenHelper.GetUserId());

        if (category == null)
            throw new ObjectNotFoundException("Category");
        
        return category;
    }
}