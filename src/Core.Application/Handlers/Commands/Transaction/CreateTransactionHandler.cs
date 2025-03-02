using Core.Application.Interfaces;
using Core.Domain.Commands.Transaction;
using Core.Domain.Exceptions;
using MediatR;

namespace Core.Application.Handlers.Commands.Transaction;

/// <summary>
/// Handles the creation of a new transaction/>.
/// </summary>
/// <remarks>
/// This handler processes the <see cref="CreateTransactionCommand"/> by:
/// 1. Extracting the current user's ID using the <see cref="ITokenHelper"/>.
/// 2. Returning the newly created <see cref="Domain.Entities.Transaction"/> entity.
/// </remarks>
public class CreateTransactionHandler(ITransactionRepository transactionRepository, ITokenHelper tokenHelper) : IRequestHandler<CreateTransactionCommand, Domain.Entities.Transaction>
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
        
        await transactionRepository.AddAsync(transaction);
        await transactionRepository.SaveChangesAsync();
        
        return transaction;
        
    }
}