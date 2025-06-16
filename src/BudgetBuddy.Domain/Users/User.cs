using System.ComponentModel.DataAnnotations;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Transactions;
using BudgetBuddy.Domain.ValueObjects;

namespace BudgetBuddy.Domain.Users;

public class User : BaseEntity
{
    [Required] [MaxLength(64)] public string Username { get; private set; }

    [MaxLength(128)] public string? FullName { get; private set; }

    [Required] [MaxLength(320)] public Email Email { get; private set; }

    [Required] [MaxLength(128)] public string Password { get; private set; }

    // Navigation properties
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Budget> Budgets { get; set; }
    public ICollection<Account> Accounts { get; set; }

    // Private constructor for EF Core
    private User()
    {
    }

    public User(string username, string? fullName, Email email, string password)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new InvalidUsernameInSignUpException();

        Username = username.Trim();
        FullName = fullName?.Trim();
        Email = email;
        Password = password;
    }
}