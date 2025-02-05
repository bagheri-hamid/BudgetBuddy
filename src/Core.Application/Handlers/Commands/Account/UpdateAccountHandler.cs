using Core.Application.Interfaces;
using Core.Domain.Commands.Account;
using MediatR;

namespace Core.Application.Handlers.Commands.Account;

/// <summary>
/// Handles the update an account by delegating the operation to the <see cref="IAccountService"/>.
/// </summary>
/// <remarks>
/// This handler processes the <see cref="UpdateAccountCommand"/> by:
/// 1. Extracting the current user's ID using the <see cref="ITokenHelper"/>.
/// 2. Delegating the account update to the <see cref="IAccountService.UpdateAccountAsync"/> method
///    with the provided name, type, balance and user ID.
/// 3. Returning the Updated <see cref="Domain.Entities.Account"/> entity.
/// </remarks>
public class UpdateAccountHandler(IAccountService accountService, ITokenHelper tokenHelper) : IRequestHandler<UpdateAccountCommand, Domain.Entities.Account>
{
    public async Task<Domain.Entities.Account> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        return await accountService.UpdateAccountAsync(request.AccountId, request.Name, request.Type, request.Balance, tokenHelper.GetUserId());
    }
}