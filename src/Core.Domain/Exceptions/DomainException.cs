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
    public ObjectNotFoundException(string objectName) : base(string.Format(Enums.MessageEnum.ObjectNotFound.GetDescription(), objectName))
    {
        StatusCode = 404;
        MessageEnum = MessageEnum.ObjectNotFound;
    }
}