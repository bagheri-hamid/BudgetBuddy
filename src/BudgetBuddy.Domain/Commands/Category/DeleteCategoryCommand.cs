using MediatR;

namespace BudgetBuddy.Domain.Commands.Category;

public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;