using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Users;
using BudgetBuddy.Domain.ValueObjects;

namespace BudgetBuddy.Domain.Transactions;

public class Transaction : BaseEntity
{
    [Required] public Money Amount { get; private set; }

    [MaxLength(256)] public string? Description { get; private set; }

    [Required] public TransactionType Type { get; private set; }
    
    [Required] public DateTime Date { get; private set; }

    // Foreign keys
    [Required] public Guid CategoryId { get; private set; }
    [Required] public Guid AccountId { get; private set; }
    [Required] public Guid UserId { get; private set; }

    // Navigation properties
    [ForeignKey("CategoryId")] public Category Category { get; set; }
    [ForeignKey("AccountId")] public Account Account { get; set; }
    [ForeignKey("UserId")] public User User { get; set; }

    // Private constructor for EF core
    private Transaction() { }

    public Transaction(Money amount, string? description, TransactionType type, DateTime date, Guid categoryId, Guid accountId, Guid userId)
    {
        if (amount < 1)
            throw new CanNotBeLessThanZeroException();
        
        Amount = amount;
        Description = description;
        Type = type;
        Date = date;
        CategoryId = categoryId;
        AccountId = accountId;
        UserId = userId;
    }
    
    public void Update(Money amount, string? description, TransactionType type, DateTime date, Guid categoryId)
    {
        if (amount < 1)
            throw new CanNotBeLessThanZeroException();

        Amount = amount;
        Description = description;
        Type = type;
        Date = date;
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        Account.UpdateBalance(Amount, Type);
        UpdatedAt = DateTime.UtcNow;
    }
}