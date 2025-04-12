using MediatR;

namespace BudgetBuddy.Domain.Commands.Budget;

public record DeleteBudgetCommand(Guid Id) : IRequest<Unit>;