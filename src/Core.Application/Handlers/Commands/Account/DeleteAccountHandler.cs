using Core.Application.Interfaces;
using Core.Domain.Commands.Account;
using Core.Domain.Exceptions;
using MediatR;

namespace Core.Application.Handlers.Commands.Account;

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