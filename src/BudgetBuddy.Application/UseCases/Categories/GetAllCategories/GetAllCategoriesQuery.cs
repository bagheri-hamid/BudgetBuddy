using BudgetBuddy.Domain.Categories;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Categories.GetAllCategories;

public record GetAllCategoriesQuery(string? Name, int Offset = 0, int Limit = 20) : IRequest<List<Category>>;