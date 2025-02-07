using MediatR;

namespace Core.Domain.Commands.Budget;

public record CreateBudgetCommand(long Amount, string Description, DateTime StartDate, DateTime EndDate, Guid? CategoryId) : IRequest<Entities.Budget>;