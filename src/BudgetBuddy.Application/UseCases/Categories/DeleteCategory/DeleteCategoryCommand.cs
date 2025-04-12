using MediatR;

namespace BudgetBuddy.Application.UseCases.Categories.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;