﻿using Core.Application.Interfaces;
using Core.Domain.Exceptions;
using Core.Domain.Queries.Category;
using MediatR;

namespace Core.Application.Handlers.Queries.Category;

public class GetCategoryByIdHandler(ICategoryRepository categoryRepository, ITokenHelper tokenHelper) : IRequestHandler<GetCategoryByIdQuery, Domain.Entities.Category>
{
    public async Task<Domain.Entities.Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FindOneAsync(c => c.Id == request.Id && c.UserId == tokenHelper.GetUserId());

        if (category == null)
            throw new ObjectNotFoundException("Category");
        
        return category;
    }
}