using MediatR;

namespace Core.Domain.Queries.Transaction;

public record GetTransactionByIdQuery(Guid Id) : IRequest<Entities.Transaction>;