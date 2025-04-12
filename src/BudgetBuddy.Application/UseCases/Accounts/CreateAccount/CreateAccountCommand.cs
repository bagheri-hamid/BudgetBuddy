using BudgetBuddy.Domain.Accounts;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Accounts.CreateAccount;

/// <summary>
/// Command for creating a new account
/// </summary>
public record CreateAccountCommand(string Name, string Type, long Balance) : IRequest<Account>;