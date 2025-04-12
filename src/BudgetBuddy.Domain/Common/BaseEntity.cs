using System.ComponentModel.DataAnnotations;

namespace BudgetBuddy.Domain.Common;

public abstract class BaseEntity
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    [Required] public bool IsDeleted { get; set; } = false;
}