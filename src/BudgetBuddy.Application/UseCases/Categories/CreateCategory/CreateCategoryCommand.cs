using BudgetBuddy.Domain.Categories;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Categories.CreateCategory;

/// <summary>
/// Command for creating a new category
/// </summary>
public record CreateCategoryCommand(string Name, Guid? ParentCategoryId) : IRequest<Category>;