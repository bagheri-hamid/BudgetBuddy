using BudgetBuddy.Domain.Budgets;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Budgets.CreateBudget;

public record CreateBudgetCommand(long Amount, string Description, DateTime StartDate, DateTime EndDate, Guid CategoryId) : IRequest<Budget>;