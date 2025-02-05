using MediatR;

namespace Core.Domain.Queries.Category;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Entities.Category>;