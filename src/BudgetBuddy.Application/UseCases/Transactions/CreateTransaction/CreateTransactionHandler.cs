using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Transactions;
using BudgetBuddy.Domain.ValueObjects;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.CreateTransaction;

/// <summary>
/// Handles the creation of a new transaction.
/// </summary>
/// <remarks>
/// Processes the <see cref="CreateTransactionCommand"/> by extracting the current user's ID
/// and returning the newly created <see cref="Transaction"/> entity.
/// </remarks>
public class CreateTransactionHandler(
    ITokenHelper tokenHelper,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateTransactionCommand, Transaction>
{
    public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        await using var transactionScope = await unitOfWork.BeginTransactionAsync();

        try
        {
            var userId = tokenHelper.GetUserId();
            var account = await unitOfWork.Repository<Domain.Accounts.Account>().FindOneAsync(a => a.Id == request.AccountId && a.UserId == userId, cancellationToken);

            if (account == null)
                throw new ObjectNotFoundException(nameof(Domain.Accounts.Account));

            if (!await unitOfWork.Repository<Domain.Categories.Category>().IsExistsAsync(c => c.Id == request.CategoryId, cancellationToken))
                throw new ObjectNotFoundException(nameof(Domain.Categories.Category));

            var transaction = new Transaction(new Money(request.Amount), request.Description, request.Type, request.Date, request.CategoryId, request.AccountId, userId);
            
            account.UpdateBalance(transaction.Amount, transaction.Type);

            await unitOfWork.Repository<Transaction>().AddAsync(transaction, cancellationToken);
            await unitOfWork.CommitAsync();

            return transaction;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}