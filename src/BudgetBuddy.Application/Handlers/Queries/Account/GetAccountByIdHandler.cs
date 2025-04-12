using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Queries.Account;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Queries.Account;

public class GetAccountByIdHandler(IAccountRepository accountRepository, ITokenHelper tokenHelper) : IRequestHandler<GetAccountByIdQuery, Domain.Accounts.Account>
{
    public async Task<Domain.Accounts.Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.FindOneAsync(c => c.Id == request.Id && c.UserId == tokenHelper.GetUserId());

        if (account == null)
            throw new ObjectNotFoundException("Account");
        
        return account;
    }
}