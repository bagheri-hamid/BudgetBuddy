using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.ValueObjects;
using MediatR;
using IAccountRepository = BudgetBuddy.Domain.Accounts.IAccountRepository;

namespace BudgetBuddy.Application.UseCases.Accounts.CreateAccount;

/// <summary>
/// Handles the creation of a new account />.
/// </summary>
/// <remarks>
/// This handler processes the <see cref="CreateAccountCommand"/> by:
/// 1. Extracting the current user's ID using the <see cref="ITokenHelper"/>.
/// 2. Returning the newly created <see cref="Domain.Accounts.Account"/> entity.
/// </remarks>
public class CreateAccountHandler(
    IAccountRepository accountRepository,
    ITokenHelper tokenHelper,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateAccountCommand, Domain.Accounts.Account>
{
    public async Task<Domain.Accounts.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new EmptyFiledException(nameof(Domain.Accounts.Account.Name));

        if (string.IsNullOrWhiteSpace(request.Type))
            throw new EmptyFiledException(nameof(Domain.Accounts.Account.Type));

        var account = new Domain.Accounts.Account(request.Name, request.Type, new Money(request.Balance), tokenHelper.GetUserId());

        await accountRepository.AddAsync(account, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return account;
    }
}