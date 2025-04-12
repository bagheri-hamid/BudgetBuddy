using MediatR;

namespace BudgetBuddy.Application.UseCases.Budgets.UpdateBudget;

public record UpdateBudgetCommand(
    Guid Id,
    long Amount,
    string Description,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<Unit>;