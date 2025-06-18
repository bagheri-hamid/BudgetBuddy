using BudgetBuddy.Domain.Extensions;
using FluentValidation.Results;

namespace BudgetBuddy.Domain.Exceptions;

public class ValidationException : DomainException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IEnumerable<ValidationFailure> failures) : base(Enums.MessageEnum.ValidationError.GetDescription())
    {
        StatusCode = 400;
        MessageEnum = Enums.MessageEnum.ValidationError;
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}