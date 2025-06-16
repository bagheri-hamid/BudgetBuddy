using System.Text.RegularExpressions;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Domain.Exceptions;

namespace BudgetBuddy.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email) ||
            !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(500)))
            throw new InvalidEmailFormatException();

        return new Email(email.ToLowerInvariant());
    }

    public static implicit operator string(Email email) => email.Value;
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}