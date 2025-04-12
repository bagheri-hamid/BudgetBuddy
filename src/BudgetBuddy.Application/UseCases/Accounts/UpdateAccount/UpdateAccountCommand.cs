using BudgetBuddy.Domain.Accounts;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Accounts.UpdateAccount;

/// <summary>
/// Command for updating an account
/// </summary>
public record UpdateAccountCommand(Guid AccountId, string Name, string Type, long Balance) : IRequest<Account>;