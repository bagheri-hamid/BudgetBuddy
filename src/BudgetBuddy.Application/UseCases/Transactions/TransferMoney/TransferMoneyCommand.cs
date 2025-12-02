using MediatR;

namespace BudgetBuddy.Application.UseCases.Transactions.TransferMoney;

public record TransferMoneyCommand(
    long Amount,
    string Description,
    DateTime Date,
    Guid SourceAccountId,
    Guid DestinationAccountId
) : IRequest<Unit>;