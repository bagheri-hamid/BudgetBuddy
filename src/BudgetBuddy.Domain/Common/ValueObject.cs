namespace BudgetBuddy.Domain.Common;

/// <summary>
/// A base class for value objects, providing structural equality
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();
    
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }
        
        var valueObject = (ValueObject)obj;
        
        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }
    
    public override int GetHashCode()
    {
        return GetEqualityComponents().Select(x => x?.GetHashCode() ?? 0).Aggregate((x, y) => x ^ y);
    }
    
    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        if (a is null && b is null)
            return true;
        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject? a, ValueObject? b)
    {
        return !(a == b);
    }
}