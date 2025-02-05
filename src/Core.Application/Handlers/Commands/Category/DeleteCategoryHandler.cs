using Core.Application.Interfaces;
using Core.Domain.Commands.Category;
using Core.Domain.Exceptions;
using MediatR;

namespace Core.Application.Handlers.Commands.Category;

public class DeleteCategoryHandler(ICategoryRepository categoryRepository, ITokenHelper tokenHelper) : IRequestHandler<DeleteCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FindOneAsync(c => c.Id == request.Id && c.UserId == tokenHelper.GetUserId());

        if (category == null)
            throw new ObjectNotFoundException("Category");
        
        category.IsDeleted = true;
        
        await categoryRepository.SaveChangesAsync();
        
        return Unit.Value;
    }
}