using MediatR;

namespace BudgetBuddy.Domain.Commands.Budget;

public record UpdateBudgetCommand(
    Guid Id,
    long Amount,
    string Description,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<Unit>;