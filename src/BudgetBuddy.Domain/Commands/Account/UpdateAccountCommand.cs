using MediatR;

namespace BudgetBuddy.Domain.Commands.Account;

/// <summary>
/// Command for updating an account
/// </summary>
public record UpdateAccountCommand(Guid AccountId, string Name, string Type, long Balance) : IRequest<Accounts.Account>;