using BudgetBuddy.Domain.Accounts;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Accounts.GetAccountById;

public record GetAccountByIdQuery(Guid Id) : IRequest<Account>;