using MediatR;

namespace BudgetBuddy.Domain.Queries.Account;

public record GetAccountByIdQuery(Guid Id) : IRequest<Entities.Account>;