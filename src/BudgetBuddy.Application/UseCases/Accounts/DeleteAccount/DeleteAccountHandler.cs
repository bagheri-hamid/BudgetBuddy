using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Exceptions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Accounts.DeleteAccount;

public class DeleteAccountHandler(IAccountRepository accountRepository, ITokenHelper tokenHelper) : IRequestHandler<DeleteAccountCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.FindOneAsync(c => c.Id == request.AccountId && c.UserId == tokenHelper.GetUserId());

        if (account == null)
            throw new ObjectNotFoundException("Account");
        
        account.IsDeleted = true;
        
        await accountRepository.SaveChangesAsync();
        
        return Unit.Value;
    }
}