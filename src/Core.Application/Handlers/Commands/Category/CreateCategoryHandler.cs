using Core.Application.Interfaces;
using Core.Domain.Commands.Category;
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
public class CreateCategoryHandler(ICategoryService categoryService, ITokenHelper tokenHelper) : IRequestHandler<CreateCategoryCommand, Domain.Entities.Category>
{
    public async Task<Domain.Entities.Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryService.CreateCategoryAsync(request.Name, request.ParentCategoryId, tokenHelper.GetUserId());
    }
}