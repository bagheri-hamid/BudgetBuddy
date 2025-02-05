using Core.Domain.Enums;
using Core.Domain.Extensions;

namespace Core.Domain.Exceptions;

public class AccountNotFoundException : DomainException
{
    public AccountNotFoundException() : base(MessageEnum.AccountNotFound.GetDescription())
    {
        StatusCode = 400;
        MessageEnum = MessageEnum.AccountNotFound;
    }
}