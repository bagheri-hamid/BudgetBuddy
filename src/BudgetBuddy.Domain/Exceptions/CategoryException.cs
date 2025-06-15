using BudgetBuddy.Domain.Extensions;

namespace BudgetBuddy.Domain.Exceptions;

public class ParentCategoryNotFoundException : DomainException
{
    public ParentCategoryNotFoundException() : base(Enums.MessageEnum.ParentCategoryNotFound.GetDescription())
    {
        StatusCode = 400;
        MessageEnum = Enums.MessageEnum.ParentCategoryNotFound;
    }
}