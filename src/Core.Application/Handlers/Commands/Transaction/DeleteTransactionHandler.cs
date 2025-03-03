using Core.Application.Interfaces;
using Core.Domain.Commands.Transaction;
using Core.Domain.Enums;
using Core.Domain.Exceptions;
using MediatR;

namespace Core.Application.Handlers.Commands.Transaction;

public class DeleteTransactionHandler(IUnitOfWork unitOfWork, ITokenHelper tokenHelper) : IRequestHandler<DeleteTransactionCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var transactionRepository = unitOfWork.Repository<Domain.Entities.Transaction>();

            var transaction = await transactionRepository.FindOneAsync(t => t.Id == request.Id && t.UserId == tokenHelper.GetUserId(), cancellationToken);
            if (transaction == null)
                throw new ObjectNotFoundException("Transaction");
            
            transaction.IsDeleted = true;
            transaction.Account.Balance += transaction.Type == TransactionType.Income ? -transaction.Amount : transaction.Amount;
            transaction.UpdatedAt = DateTime.Now;
            transaction.Account.UpdatedAt = DateTime.Now;
            
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