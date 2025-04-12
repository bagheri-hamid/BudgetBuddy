using MediatR;

namespace BudgetBuddy.Domain.Commands.Account;

public record DeleteAccountCommand(Guid AccountId) : IRequest<Unit>;