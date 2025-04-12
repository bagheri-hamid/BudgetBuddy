using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBuddy.Domain.Entities;

public class Budget : BaseEntity
{
    [Required] public long Amount { get; set; }
    [MaxLength(256)] public string? Description { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    
    // Foreign keys
    [Required] public Guid CategoryId { get; set; }
    [Required] public Guid UserId { get; set; }

    // Navigation properties
    [ForeignKey("CategoryId")] public Category Category { get; set; }
    [ForeignKey("UserId")] public User User { get; set; }
}