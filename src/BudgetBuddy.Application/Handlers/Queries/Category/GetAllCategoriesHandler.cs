﻿using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Queries.Category;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Queries.Category;

public class GetAllCategoriesHandler(
    ICategoryRepository categoryRepository,
    ITokenHelper tokenHelper
) : IRequestHandler<GetAllCategoriesQuery, List<Domain.Categories.Category>>
{
    public async Task<List<Domain.Categories.Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.FindAsync(
            c =>
                c.UserId == tokenHelper.GetUserId() &&
                (string.IsNullOrWhiteSpace(request.Name) || c.Name == request.Name),
            request.Offset,
            request.Limit
        );
        return categories;
    }
}