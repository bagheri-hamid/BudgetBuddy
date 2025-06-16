using BudgetBuddy.Domain.Common;

namespace BudgetBuddy.Domain.ValueObjects;

public class Money : ValueObject
{
    // IRR (Iranian rial)
    public long Amount { get; }

    public Money(long amount)
    {
        Amount = amount;
    }
    
    public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount);
    public static Money operator-(Money a, Money b) => new(a.Amount - b.Amount);
    public static implicit operator long(Money a) => a.Amount;
    public static explicit operator Money(long amount) => new(amount);
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}