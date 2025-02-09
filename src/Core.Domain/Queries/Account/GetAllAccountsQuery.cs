using MediatR;

namespace Core.Domain.Queries.Account;

public record GetAllAccountsQuery(string? Name, int Offset = 0, int Limit = 20) : IRequest<List<Entities.Account>>;