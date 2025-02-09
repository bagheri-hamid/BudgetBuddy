using MediatR;

namespace Core.Domain.Queries.Account;

public record GetAccountByIdQuery(Guid Id) : IRequest<Entities.Account>;