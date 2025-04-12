using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Commands.Transaction;
using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Transactions;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Commands.Transaction;

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
) : IRequestHandler<CreateTransactionCommand, Domain.Transactions.Transaction>
{
    public async Task<Domain.Transactions.Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount < 1)
            throw new CanNotBeLessThanZeroException();

        var transaction = new Domain.Transactions.Transaction
        {
            Amount = request.Amount,
            Description = request.Description,
            Type = request.Type,
            Date = request.Date,
            CategoryId = request.CategoryId,
            AccountId = request.AccountId,
            UserId = tokenHelper.GetUserId(),
        };

        await unitOfWork.BeginTransactionAsync();

        try
        {
            var transactionRepository = unitOfWork.Repository<Domain.Transactions.Transaction>();
            var accountRepository = unitOfWork.Repository<Domain.Accounts.Account>();
            var categoryRepository = unitOfWork.Repository<Domain.Categories.Category>();

            // Validate that the category exists
            if (!await categoryRepository.IsExistsAsync(c => c.Id == request.CategoryId, cancellationToken))
                throw new ObjectNotFoundException("Category");

            // Add the transaction
            await transactionRepository.AddAsync(transaction, cancellationToken);

            // Update the account balance
            var account = await accountRepository.FindOneAsync(a => a.Id == request.AccountId && a.UserId == transaction.UserId, cancellationToken);
            if (account == null)
                throw new ObjectNotFoundException("Account");

            account.Balance += request.Type == TransactionType.Income ? request.Amount : -request.Amount;
            account.UpdatedAt = DateTime.UtcNow;
            
            await unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }

        return transaction;
    }
}