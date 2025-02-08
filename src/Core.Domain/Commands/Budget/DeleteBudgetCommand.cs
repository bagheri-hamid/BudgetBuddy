using MediatR;

namespace Core.Domain.Commands.Budget;

public record DeleteBudgetCommand(Guid Id) : IRequest<Unit>;