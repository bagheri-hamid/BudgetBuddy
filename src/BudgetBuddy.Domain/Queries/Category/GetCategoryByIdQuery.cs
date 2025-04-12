using MediatR;

namespace BudgetBuddy.Domain.Queries.Category;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Categories.Category>;