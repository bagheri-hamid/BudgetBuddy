using Core.Application.Interfaces;
using Core.Domain.Commands.Category;
using Core.Domain.Exceptions;
using MediatR;

namespace Core.Application.Handlers.Commands.Category;

/// <summary>
/// Handles the creation of a new category by delegating the operation to the <see cref="ICategoryService"/>.
/// </summary>
/// <remarks>
/// This handler processes the <see cref="CreateCategoryCommand"/> by:
/// 1. Extracting the current user's ID using the <see cref="ITokenHelper"/>.
/// 2. Delegating the category creation to the <see cref="ICategoryService.CreateCategoryAsync"/> method
///    with the provided name, parent category ID, and user ID.
/// 3. Returning the newly created <see cref="Domain.Entities.Category"/> entity.
/// </remarks>
public class CreateCategoryHandler(ICategoryRepository categoryRepository, ITokenHelper tokenHelper) : IRequestHandler<CreateCategoryCommand, Domain.Entities.Category>
{
    public async Task<Domain.Entities.Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new EmptyFiledException(nameof(Domain.Entities.Category.Name));

        if (request.ParentCategoryId != null)
        {
            var isExist = await categoryRepository.IsExistsAsync(c => c.Id == request.ParentCategoryId);

            if (!isExist)
                throw new ParentCategoryNotFoundException();
        }

        var category = new Domain.Entities.Category
        {
            Name = request.Name,
            ParentCategoryId = request.ParentCategoryId,
            UserId = tokenHelper.GetUserId(),
        };
        
        await categoryRepository.AddAsync(category);
        await categoryRepository.SaveChangesAsync();
        
        return category;
        
    }
}