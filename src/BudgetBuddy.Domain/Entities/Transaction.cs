using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetBuddy.Domain.Enums;

namespace BudgetBuddy.Domain.Entities;

public class Transaction : BaseEntity
{
    [Required] public long Amount { get; set; }

    [MaxLength(256)] public string? Description { get; set; }

    [Required] public TransactionType Type { get; set; }
    
    [Required] public required DateTime Date { get; set; }

    // Foreign keys
    [Required] public Guid CategoryId { get; set; }
    [Required] public Guid AccountId { get; set; }
    [Required] public Guid UserId { get; set; }

    // Navigation properties
    [ForeignKey("CategoryId")] public Category Category { get; set; }
    [ForeignKey("AccountId")] public Account Account { get; set; }
    [ForeignKey("UserId")] public User User { get; set; }
}