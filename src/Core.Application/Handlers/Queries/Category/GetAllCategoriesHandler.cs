using Core.Application.Interfaces;
using Core.Domain.Queries.Category;
using MediatR;

namespace Core.Application.Handlers.Queries.Category;

public class GetAllCategoriesHandler(
    ICategoryRepository categoryRepository,
    ITokenHelper tokenHelper
) : IRequestHandler<GetAllCategoriesQuery, List<Domain.Entities.Category>>
{
    public async Task<List<Domain.Entities.Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
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