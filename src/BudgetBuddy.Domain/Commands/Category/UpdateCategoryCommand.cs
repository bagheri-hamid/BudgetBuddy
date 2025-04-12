using MediatR;

namespace BudgetBuddy.Domain.Commands.Category;

public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Categories.Category>;