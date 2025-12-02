using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Transactions;
using BudgetBuddy.Domain.ValueObjects;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.TransferMoney;

public class TransferMoneyHandler(IUnitOfWork unitOfWork, ITokenHelper tokenHelper) : IRequestHandler<TransferMoneyCommand, Unit>
{
    public async Task<Unit> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount < 1)
            throw new CanNotBeLessThanZeroException();

        if (request.SourceAccountId == request.DestinationAccountId)
            throw new TransferDestinationException();

        await using var transactionScope = await unitOfWork.BeginTransactionAsync();

        try
        {
            var userId = tokenHelper.GetUserId();
            var accountRepo = unitOfWork.Repository<Account>();
            var transactionRepo = unitOfWork.Repository<Transaction>();
            
            var sourceAccount = await accountRepo.FindOneAsync(acc => acc.Id == request.SourceAccountId && acc.UserId == userId, cancellationToken)
                ?? throw new ObjectNotFoundException("Source Account");
            var destinationAccount = await accountRepo.FindOneAsync(acc => acc.Id == request.DestinationAccountId && acc.UserId == userId, cancellationToken)
                ?? throw new ObjectNotFoundException("Destination Account");

            if (sourceAccount.Balance.Amount < request.Amount)
                throw new InvalidValueException("Insufficient funds in source account.");
            
            var money = new Money(request.Amount);
            
            // Outgoing
            var debitTx = new Transaction(
                money,
                request.Description,
                TransactionType.Transfer,
                request.Date,
                null,
                request.SourceAccountId,
                userId
            );
            
            // Incoming
            var creditTx = new Transaction(
                money, 
                request.Description, 
                TransactionType.Transfer, 
                request.Date, 
                null,
                request.DestinationAccountId, 
                userId
            );
            
            sourceAccount.UpdateBalance(money, TransactionType.Expense); 
            destinationAccount.UpdateBalance(money, TransactionType.Income);
            
            await transactionRepo.AddAsync(debitTx, cancellationToken);
            await transactionRepo.AddAsync(creditTx, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            await unitOfWork.CommitAsync();
            
            return Unit.Value;
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}