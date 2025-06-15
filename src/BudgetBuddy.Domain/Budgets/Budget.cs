using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Users;

namespace BudgetBuddy.Domain.Budgets;

public class Budget : BaseEntity
{
    [Required] public long Amount { get; private set; }
    [MaxLength(256)] public string? Description { get; private set; }
    [Required] public DateTime StartDate { get; private set; }
    [Required] public DateTime EndDate { get; private set; }

    // Foreign keys
    [Required] public Guid CategoryId { get; private set; }
    [Required] public Guid UserId { get; private set; }

    // Navigation properties
    [ForeignKey("CategoryId")] public Category Category { get; set; }
    [ForeignKey("UserId")] public User User { get; set; }

    // Private constructor for EF core
    private Budget()
    {
    }

    public Budget(long amount, string? description, DateTime startDate, DateTime endDate, Guid categoryId, Guid userId)
    {
        if (amount < 1) throw new CanNotBeLessThanZeroException();
        if (startDate == default || endDate == default || endDate <= startDate) throw new InvalidDateException();

        Amount = amount;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        CategoryId = categoryId;
        UserId = userId;
    }

    public void Update(long amount, string? description, DateTime startDate, DateTime endDate)
    {
        if (amount < 1) throw new CanNotBeLessThanZeroException();
        if (startDate == default || endDate == default || endDate <= startDate) throw new InvalidDateException();

        Amount = amount;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }
}