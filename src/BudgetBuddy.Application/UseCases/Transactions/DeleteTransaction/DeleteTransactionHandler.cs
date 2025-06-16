using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Exceptions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.DeleteTransaction;

public class DeleteTransactionHandler(IUnitOfWork unitOfWork, ITokenHelper tokenHelper) : IRequestHandler<DeleteTransactionCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var transactionRepository = unitOfWork.Repository<Domain.Transactions.Transaction>();

            var transaction = await transactionRepository.FindOneAsync(
                t => t.Id == request.Id && t.UserId == tokenHelper.GetUserId(),
                cancellationToken,
                t => t.Account
            );

            if (transaction == null)
                throw new ObjectNotFoundException("Transaction");

            transaction.Account.RevertTransaction(transaction);
            transaction.Delete();

            await unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }

        return Unit.Value;
    }
}