using BudgetBuddy.Domain.Categories;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Categories.UpdateCategory;

public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Category>;