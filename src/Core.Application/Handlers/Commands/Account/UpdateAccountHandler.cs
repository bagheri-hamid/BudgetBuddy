using Core.Application.Interfaces;
using Core.Domain.Commands.Account;
using Core.Domain.Exceptions;
using MediatR;

namespace Core.Application.Handlers.Commands.Account;

/// <summary>
/// Handles the update an account />.
/// </summary>
/// <remarks>
/// This handler processes the <see cref="UpdateAccountCommand"/> by:
/// 1. Extracting the current user's ID using the <see cref="ITokenHelper"/>.
/// 2. Returning the Updated <see cref="Domain.Entities.Account"/> entity.
/// </remarks>
public class UpdateAccountHandler(IAccountRepository accountRepository, ITokenHelper tokenHelper) : IRequestHandler<UpdateAccountCommand, Domain.Entities.Account>
{
    public async Task<Domain.Entities.Account> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new EmptyFiledException(nameof(Domain.Entities.Account.Name));

        if (string.IsNullOrWhiteSpace(request.Type))
            throw new EmptyFiledException(nameof(Domain.Entities.Account.Type));
        
        if (request.Balance < 0)
            throw new InvalidValueException(nameof(Domain.Entities.Account.Balance));

        var account = await accountRepository.FindOneAsync(c => c.Id == request.AccountId && c.UserId == tokenHelper.GetUserId());
    
        if (account == null)
            throw new ObjectNotFoundException("Account");
        
        account.Name = request.Name;
        account.Type = request.Type;
        account.Balance = request.Balance;
        account.UpdatedAt = DateTime.UtcNow;
        
        await accountRepository.SaveChangesAsync();

        return account;
    }
}