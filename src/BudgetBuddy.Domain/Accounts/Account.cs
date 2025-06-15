using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Transactions;
using BudgetBuddy.Domain.Users;

namespace BudgetBuddy.Domain.Accounts;

public class Account : BaseEntity
{
    [Required] [MaxLength(100)] public string Name { get; private set; } = null!;
    [Required] [MaxLength(100)] public string Type { get; private set; } = null!; // Checking, Savings, Credit Card, etc.
    public long Balance { get; private set; } = 0;

    // Foreign key
    [Required] public Guid UserId { get; private set; }

    // Navigation properties
    [ForeignKey("UserId")] public User User { get; set; }
    public ICollection<Transaction> Transactions { get; set; }

    // Private constructor for EF Core
    private Account()
    {
    }

    public Account(string name, string type, long balance, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new EmptyFiledException(nameof(Name));
        if (string.IsNullOrWhiteSpace(type)) throw new EmptyFiledException(nameof(Type));
        if (balance < 0) throw new InvalidValueException(nameof(Balance));

        Name = name;
        Type = type;
        Balance = balance;
        UserId = userId;
    }
    
    public void Update(string name, string type, long balance)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new EmptyFiledException(nameof(Name));
        if (string.IsNullOrWhiteSpace(type)) throw new EmptyFiledException(nameof(Type));
        
        Name = name;
        Type = type;
        Balance = balance;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateBalance(long amount, TransactionType type)
    {
        Balance += type == TransactionType.Income ? amount : -amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        UpdatedAt = DateTime.UtcNow;
        IsDeleted = true;
    }
    
    // Recalculating account's balance when transaction updates
    public void RecalculateBalance(TransactionType type, long oldTransactionAmount, long newTransactionAmount)
    {
        // Reverting account balance
        Balance += type == TransactionType.Income ? -oldTransactionAmount : oldTransactionAmount;
        
        // Recalculating account balance
        Balance += type == TransactionType.Income ? newTransactionAmount : -newTransactionAmount;
        UpdatedAt = DateTime.UtcNow;
    }
}