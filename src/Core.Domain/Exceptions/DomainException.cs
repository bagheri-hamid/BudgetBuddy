using Core.Domain.Enums;
using Core.Domain.Extensions;

namespace Core.Domain.Exceptions;

public abstract class DomainException(string message) : Exception(message)
{
    public int StatusCode { get; set; }
    public MessageEnum MessageEnum { get; set; }
}

public class EmptyNameException : DomainException
{
    public EmptyNameException() : base(MessageEnum.NameIsEmpty.GetDescription())
    {
        StatusCode = 400;
        MessageEnum = MessageEnum.NameIsEmpty;
    }
}