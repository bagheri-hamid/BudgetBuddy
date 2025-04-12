using BudgetBuddy.Domain.Accounts;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Accounts.GetAllAccounts;

public record GetAllAccountsQuery(string? Name, int Offset = 0, int Limit = 20) : IRequest<List<Account>>;