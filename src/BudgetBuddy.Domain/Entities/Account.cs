using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBuddy.Domain.Entities;

public class Account : BaseEntity
{
    [Required] [MaxLength(100)] public string Name { get; set; } = null!;
    [Required] [MaxLength(100)] public string Type { get; set; } = null!; // Checking, Savings, Credit Card, etc.
    public long Balance { get; set; } = 0;

    // Foreign key
    [Required] public Guid UserId { get; set; }

    // Navigation properties
    [ForeignKey("UserId")] public User User { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}