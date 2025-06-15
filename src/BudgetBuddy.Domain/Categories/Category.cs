using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Transactions;
using BudgetBuddy.Domain.Users;

namespace BudgetBuddy.Domain.Categories;

public class Category : BaseEntity
{
    [Required] [MaxLength(100)] public string Name { get; private set; } = null!;

    public Guid? ParentCategoryId { get; private set; }

    // Foreign key
    [Required] public Guid UserId { get; private set; }

    // Navigation properties
    [ForeignKey("UserId")] public User User { get; set; }
    public Category ParentCategory { get; set; }
    public ICollection<Category> ChildCategories { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Budget> Budgets { get; set; }
    
    // Private constructor for EF core
    private Category() { }

    public Category(string name, Guid? parentCategoryId, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new EmptyFiledException(nameof(Name));

        Name = name;
        ParentCategoryId = parentCategoryId;
        UserId = userId;
    }
    
    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new EmptyFiledException(nameof(Name));
        
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        UpdatedAt = DateTime.UtcNow;
        IsDeleted = true;
    }
}