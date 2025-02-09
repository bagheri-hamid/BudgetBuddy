using Core.Application.Interfaces;
using Core.Domain.Queries.Account;
using MediatR;

namespace Core.Application.Handlers.Queries.Account;

public class GetAllAccountsHandler(
    IAccountRepository accountRepository,
    ITokenHelper tokenHelper
) : IRequestHandler<GetAllAccountsQuery, List<Domain.Entities.Account>>
{
    public async Task<List<Domain.Entities.Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
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