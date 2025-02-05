using Core.Domain.Enums;
using Core.Domain.Extensions;

namespace Core.Domain.Exceptions;

public abstract class DomainException(string message) : Exception(message)
{
    public int StatusCode { get; set; }
    public MessageEnum MessageEnum { get; set; }
}

public class EmptyFiledException : DomainException
{
    public EmptyFiledException(string filedName) : base(string.Format(MessageEnum.FiledIsEmpty.GetDescription(), filedName))
    {
        StatusCode = 400;
        MessageEnum = MessageEnum.FiledIsEmpty;
    }
}

public class InvalidValueException : DomainException
{
    public InvalidValueException(string filedName) : base(string.Format(MessageEnum.ValueIsInvalid.GetDescription(), filedName))
    {
        StatusCode = 400;
        MessageEnum = MessageEnum.ValueIsInvalid;
    }
}