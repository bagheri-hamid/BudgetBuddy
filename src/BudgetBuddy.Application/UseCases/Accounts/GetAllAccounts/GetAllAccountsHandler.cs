using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Accounts;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Accounts.GetAllAccounts;

public class GetAllAccountsHandler(
    IAccountRepository accountRepository,
    ITokenHelper tokenHelper
) : IRequestHandler<GetAllAccountsQuery, List<Domain.Accounts.Account>>
{
    public async Task<List<Domain.Accounts.Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await accountRepository.FindAsync(
            c =>
                c.UserId == tokenHelper.GetUserId() &&
                (string.IsNullOrWhiteSpace(request.Name) || c.Name.Contains(request.Name)),
            request.Offset,
            request.Limit
        );
        return accounts;
    }
}