using MediatR;

namespace BudgetBuddy.Application.UseCases.Accounts.DeleteAccount;

public record DeleteAccountCommand(Guid AccountId) : IRequest<Unit>;