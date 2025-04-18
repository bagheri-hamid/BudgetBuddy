﻿using System.ComponentModel.DataAnnotations;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Domain.Transactions;

namespace BudgetBuddy.Domain.Users;

public class User : BaseEntity
{
    [Required] [MaxLength(64)] public required string Username { get; set; }

    [MaxLength(128)] public string? FullName { get; set; }

    [Required] [MaxLength(320)] public required string Email { get; set; }

    [Required] [MaxLength(128)] public required string Password { get; set; }
    
    // Navigation properties
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Budget> Budgets { get; set; }
    public ICollection<Account> Accounts { get; set; }
}