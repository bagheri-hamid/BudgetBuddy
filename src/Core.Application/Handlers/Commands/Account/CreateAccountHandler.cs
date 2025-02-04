using Core.Application.Interfaces;
using Core.Domain.Commands.Account;
using MediatR;

namespace Core.Application.Handlers.Commands.Account;

/// <summary>
/// Handles the creation of a new account by delegating the operation to the <see cref="IAccountService"/>.
/// </summary>
/// <remarks>
/// This handler processes the <see cref="CreateAccountCommand"/> by:
/// 1. Extracting the current user's ID using the <see cref="ITokenHelper"/>.
/// 2. Delegating the account creation to the <see cref="IAccountService.CreateAccountAsync"/> method
///    with the provided name, type, balance and user ID.
/// 3. Returning the newly created <see cref="Domain.Entities.Account"/> entity.
/// </remarks>
public class CreateAccountHandler(IAccountService accountService, ITokenHelper tokenHelper) : IRequestHandler<CreateAccountCommand, Domain.Entities.Account>
{
    public async Task<Domain.Entities.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        return await accountService.CreateAccountAsync(request.Name, request.Type, request.Balance, tokenHelper.GetUserId());
    }
}