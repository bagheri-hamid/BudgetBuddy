using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Transactions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.GetTransactionById;

public class GetTransactionByIdHandler(ITransactionRepository transactionRepository, ITokenHelper tokenHelper)
    : IRequestHandler<GetTransactionByIdQuery, Domain.Transactions.Transaction>
{
    public async Task<Domain.Transactions.Transaction> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.FindOneAsync(t => t.Id == request.Id && t.UserId == tokenHelper.GetUserId(), cancellationToken);

        if (transaction == null)
            throw new ObjectNotFoundException("Transaction");
        
        return transaction;
    }
}