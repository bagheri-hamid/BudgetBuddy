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

public class ObjectNotFoundException : DomainException
{
    public ObjectNotFoundException(string objectName) : base(string.Format(MessageEnum.ObjectNotFound.GetDescription(), objectName))
    {
        StatusCode = 404;
        MessageEnum = MessageEnum.ObjectNotFound;
    }
}

public class InvalidValueException : DomainException
{
    public InvalidValueException(string fieldName) : base(string.Format(MessageEnum.ValueIsInvalid.GetDescription(), fieldName))
    {
        StatusCode = 400;
        MessageEnum = MessageEnum.ValueIsInvalid;
    }
}

public class CanNotBeLessThanZeroException : DomainException
{
    public CanNotBeLessThanZeroException() : base(MessageEnum.CanNotBeLessThanZero.GetDescription())
    {
        StatusCode = 400;
        MessageEnum = MessageEnum.CanNotBeLessThanZero;
    }
}