using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Exceptions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Categories.DeleteCategory;

public class DeleteCategoryHandler(
    ICategoryRepository categoryRepository,
    ITokenHelper tokenHelper,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FindOneAsync(c => c.Id == request.Id && c.UserId == tokenHelper.GetUserId(), cancellationToken);

        if (category == null)
            throw new ObjectNotFoundException("Category");

        category.Delete();

        await unitOfWork.CompleteAsync(cancellationToken);

        return Unit.Value;
    }
}