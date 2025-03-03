using Core.Application.Interfaces;
using Core.Domain.Commands.Transaction;
using Core.Domain.Enums;
using Core.Domain.Exceptions;
using MediatR;

namespace Core.Application.Handlers.Commands.Transaction;

/// <summary>
/// Handles the creation of a new transaction.
/// </summary>
/// <remarks>
/// Processes the <see cref="CreateTransactionCommand"/> by extracting the current user's ID
/// and returning the newly created <see cref="Domain.Entities.Transaction"/> entity.
/// </remarks>
public class CreateTransactionHandler(
    ITokenHelper tokenHelper,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateTransactionCommand, Domain.Entities.Transaction>
{
    public async Task<Domain.Entities.Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount < 1)
            throw new CanNotBeLessThanZeroException();

        var transaction = new Domain.Entities.Transaction
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
            var transactionRepository = unitOfWork.Repository<Domain.Entities.Transaction>();
            var accountRepository = unitOfWork.Repository<Domain.Entities.Account>();
            var categoryRepository = unitOfWork.Repository<Domain.Entities.Category>();

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