using MediatR;

namespace BudgetBuddy.Domain.Queries.Budget;

public record GetAllBudgetsQuery(long? Amount, string? Description, int Offset = 0, int Limit = 20) : IRequest<List<Entities.Budget>>;