using BudgetBuddy.Domain.Budgets;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Budgets.GetAllBudgets;

public record GetAllBudgetsQuery(long? Amount, string? Description, int Offset = 0, int Limit = 20) : IRequest<List<Budget>>;