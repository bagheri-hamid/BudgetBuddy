using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Domain.Transactions;
using BudgetBuddy.Domain.Users;

namespace BudgetBuddy.Domain.Categories;

public class Category : BaseEntity
{
    [Required] [MaxLength(100)] public string Name { get; set; } = null!;

    public Guid? ParentCategoryId { get; set; }

    // Foreign key
    [Required] public Guid UserId { get; set; }

    // Navigation properties
    [ForeignKey("UserId")] public User User { get; set; }
    public Category ParentCategory { get; set; }
    public ICollection<Category> ChildCategories { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Budget> Budgets { get; set; }
}