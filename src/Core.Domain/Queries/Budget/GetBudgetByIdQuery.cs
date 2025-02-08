using MediatR;

namespace Core.Domain.Queries.Budget;

public record GetBudgetByIdQuery(Guid Id) : IRequest<Entities.Budget>;