using BudgetBuddy.Domain.Categories;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Categories.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Category>;