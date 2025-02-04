using Core.Domain.Enums;
using Core.Domain.Extensions;

namespace Core.Domain.Exceptions;

public class ParentCategoryNotFoundException : DomainException
{
    public ParentCategoryNotFoundException() : base(MessageEnum.ParentCategoryNotFound.GetDescription())
    {
        StatusCode = 400;
        MessageEnum = MessageEnum.ParentCategoryNotFound;
    }
}