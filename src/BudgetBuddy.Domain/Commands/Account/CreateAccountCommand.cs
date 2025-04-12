using MediatR;

namespace BudgetBuddy.Domain.Commands.Account;

/// <summary>
/// Command for creating a new account
/// </summary>
public record CreateAccountCommand(string Name, string Type, long Balance) : IRequest<Accounts.Account>;