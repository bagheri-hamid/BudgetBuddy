using MediatR;

namespace BudgetBuddy.Application.UseCases.Budgets.DeleteBudget;

public record DeleteBudgetCommand(Guid Id) : IRequest<Unit>;