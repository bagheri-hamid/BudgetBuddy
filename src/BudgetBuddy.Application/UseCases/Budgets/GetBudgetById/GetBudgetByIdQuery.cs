using BudgetBuddy.Domain.Budgets;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Budgets.GetBudgetById;

public record GetBudgetByIdQuery(Guid Id) : IRequest<Budget>;