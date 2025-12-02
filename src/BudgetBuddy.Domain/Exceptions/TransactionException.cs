using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Extensions;

namespace BudgetBuddy.Domain.Exceptions;

public class TransferDestinationException : DomainException
{
    public TransferDestinationException() : base(MessageEnum.TransferDestinationIsSameWithSource.GetDescription())
    {
        StatusCode = 400;
        MessageEnum = MessageEnum.TransferDestinationIsSameWithSource;
    }
}