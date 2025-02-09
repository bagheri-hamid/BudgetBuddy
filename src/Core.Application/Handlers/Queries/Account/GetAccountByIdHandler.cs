using Core.Application.Interfaces;
using Core.Domain.Exceptions;
using Core.Domain.Queries.Account;
using MediatR;

namespace Core.Application.Handlers.Queries.Account;

public class GetAccountByIdHandler(IAccountRepository accountRepository, ITokenHelper tokenHelper) : IRequestHandler<GetAccountByIdQuery, Domain.Entities.Account>
{
    public async Task<Domain.Entities.Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.FindOneAsync(c => c.Id == request.Id && c.UserId == tokenHelper.GetUserId());

        if (account == null)
            throw new ObjectNotFoundException("Account");
        
        return account;
    }
}