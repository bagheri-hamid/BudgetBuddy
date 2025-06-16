using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.ValueObjects;
using MediatR;
using IAccountRepository = BudgetBuddy.Domain.Accounts.IAccountRepository;

namespace BudgetBuddy.Application.UseCases.Accounts.UpdateAccount;

/// <summary>
/// Handles the update an account />.
/// </summary>
/// <remarks>
/// This handler processes the <see cref="UpdateAccountCommand"/> by:
/// 1. Extracting the current user's ID using the <see cref="ITokenHelper"/>.
/// 2. Returning the Updated <see cref="Account"/> entity.
/// </remarks>
public class UpdateAccountHandler(
    IAccountRepository accountRepository,
    ITokenHelper tokenHelper,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateAccountCommand, Account>
{
    public async Task<Account> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new EmptyFiledException(nameof(Account.Name));

        if (string.IsNullOrWhiteSpace(request.Type))
            throw new EmptyFiledException(nameof(Account.Type));

        if (request.Balance < 0)
            throw new InvalidValueException(nameof(Account.Balance));

        var account = await accountRepository.FindOneAsync(c => c.Id == request.AccountId && c.UserId == tokenHelper.GetUserId(), cancellationToken);

        if (account == null)
            throw new ObjectNotFoundException("Account");

        account.Update(request.Name, request.Type, new Money(request.Balance));

        await unitOfWork.CompleteAsync(cancellationToken);

        return account;
    }
}