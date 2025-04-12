using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Commands.Account;
using BudgetBuddy.Domain.Exceptions;
using MediatR;
using IAccountRepository = BudgetBuddy.Domain.Accounts.IAccountRepository;

namespace BudgetBuddy.Application.Handlers.Commands.Account;

/// <summary>
/// Handles the creation of a new account />.
/// </summary>
/// <remarks>
/// This handler processes the <see cref="CreateAccountCommand"/> by:
/// 1. Extracting the current user's ID using the <see cref="ITokenHelper"/>.
/// 2. Returning the newly created <see cref="Account"/> entity.
/// </remarks>
public class CreateAccountHandler(IAccountRepository accountRepository, ITokenHelper tokenHelper) : IRequestHandler<CreateAccountCommand, Domain.Accounts.Account>
{
    public async Task<Domain.Accounts.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new EmptyFiledException(nameof(Domain.Accounts.Account.Name));

        if (string.IsNullOrWhiteSpace(request.Type))
            throw new EmptyFiledException(nameof(Domain.Accounts.Account.Type));
        
        var account = new Domain.Accounts.Account
        {
            Name = request.Name,
            Type = request.Type,
            Balance = request.Balance,
            UserId = tokenHelper.GetUserId(),
        };
        
        await accountRepository.AddAsync(account);
        await accountRepository.SaveChangesAsync();
        
        return account;
    }
}