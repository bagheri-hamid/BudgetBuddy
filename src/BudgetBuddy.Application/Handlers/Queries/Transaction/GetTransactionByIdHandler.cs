using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Queries.Transaction;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Queries.Transaction;

public class GetTransactionByIdHandler(ITransactionRepository transactionRepository, ITokenHelper tokenHelper)
    : IRequestHandler<GetTransactionByIdQuery, Domain.Entities.Transaction>
{
    public async Task<Domain.Entities.Transaction> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.FindOneAsync(t => t.Id == request.Id && t.UserId == tokenHelper.GetUserId(), cancellationToken);

        if (transaction == null)
            throw new ObjectNotFoundException("Transaction");
        
        return transaction;
    }
}