using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Domain.Users;

namespace BudgetBuddy.Domain.Budgets;

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