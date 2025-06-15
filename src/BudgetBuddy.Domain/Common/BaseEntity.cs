using System.ComponentModel.DataAnnotations;

namespace BudgetBuddy.Domain.Common;

public abstract class BaseEntity
{
    [Key] public Guid Id { get; private set; } = Guid.NewGuid();

    [Required] public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; protected set; }

    [Required] public bool IsDeleted { get; protected set; }
}