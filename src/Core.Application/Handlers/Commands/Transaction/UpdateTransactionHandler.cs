﻿using Core.Application.Interfaces;
using Core.Domain.Commands.Transaction;
using Core.Domain.Enums;
using Core.Domain.Exceptions;
using MediatR;

namespace Core.Application.Handlers.Commands.Transaction;

public class UpdateTransactionHandler(IUnitOfWork unitOfWork, ITokenHelper tokenHelper) : IRequestHandler<UpdateTransactionCommand, Domain.Entities.Transaction>
{
    public async Task<Domain.Entities.Transaction> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Transaction? transaction;
        
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var transactionRepository = unitOfWork.Repository<Domain.Entities.Transaction>();
            var categoryRepository = unitOfWork.Repository<Domain.Entities.Category>();

            // Validate that the category exists
            if (!await categoryRepository.IsExistsAsync(c => c.Id == request.CategoryId, cancellationToken))
                throw new ObjectNotFoundException("Category");
            
            transaction = await transactionRepository.FindOneAsync(
                t => t.Id == request.TransActionId && t.UserId == tokenHelper.GetUserId(),
                cancellationToken,
                t => t.Account);
            
            if (transaction == null)
                throw new ObjectNotFoundException("Transaction");
            
            transaction.Amount = request.Amount;
            transaction.Description = request.Description;
            transaction.Type = request.Type;
            transaction.Date = request.Date;
            transaction.CategoryId = request.CategoryId;
            transaction.UpdatedAt = DateTime.UtcNow;
            
            // Reverting account balance
            transaction.Account.Balance += transaction.Type == TransactionType.Income ? -transaction.Amount : transaction.Amount;
            
            // Recalculating account balance
            transaction.Account.Balance += request.Type == TransactionType.Income ? request.Amount : -request.Amount;
            transaction.Account.UpdatedAt = DateTime.UtcNow;
            
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