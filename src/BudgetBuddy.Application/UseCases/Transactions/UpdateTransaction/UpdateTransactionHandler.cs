using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.ValueObjects;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionHandler(IUnitOfWork unitOfWork, ITokenHelper tokenHelper) : IRequestHandler<UpdateTransactionCommand, Domain.Transactions.Transaction>
{
    public async Task<Domain.Transactions.Transaction> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var transactionRepository = unitOfWork.Repository<Domain.Transactions.Transaction>();
            var categoryRepository = unitOfWork.Repository<Domain.Categories.Category>();

            // Validate that the category exists
            if (!await categoryRepository.IsExistsAsync(c => c.Id == request.CategoryId, cancellationToken))
                throw new ObjectNotFoundException("Category");

            var transaction = await transactionRepository.FindOneAsync(
                t => t.Id == request.TransActionId && t.UserId == tokenHelper.GetUserId(),
                cancellationToken,
                t => t.Account);

            if (transaction == null)
                throw new ObjectNotFoundException("Transaction");

            var requestAmountValueObject = new Money(request.Amount);
            
            transaction.Update(requestAmountValueObject, request.Description, request.Type, request.Date, request.CategoryId);

            transaction.Account.RecalculateBalance(transaction.Type, transaction.Amount, requestAmountValueObject);

            await unitOfWork.CommitAsync();
            return transaction;
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}