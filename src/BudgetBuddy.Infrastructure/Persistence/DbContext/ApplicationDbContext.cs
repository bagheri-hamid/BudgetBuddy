using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Domain.Transactions;
using BudgetBuddy.Domain.Users;
using BudgetBuddy.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Infrastructure.Persistence.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User relationships
        modelBuilder.Entity<Account>()
            .HasOne(a => a.User)
            .WithMany(u => u.Accounts)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Budget>()
            .HasOne(b => b.User)
            .WithMany(u => u.Budgets)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Transaction's User with Restrict to avoid multiple cascade paths
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Category's self-referencing relationship
        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.ChildCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);

        // Transaction relationships
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Account)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        // Budget relationship
        modelBuilder.Entity<Budget>()
            .HasOne(b => b.Category)
            .WithMany(c => c.Budgets)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // .: VALUE OBJECTS CONFIGURATIONS :.

        modelBuilder.Entity<Account>(builder =>
        {
            builder.Property(t => t.Balance)
                .HasConversion(money => money.Amount, dbValue => new Money(dbValue));
        });

        modelBuilder.Entity<Transaction>(builder =>
        {
            builder.Property(t => t.Amount)
                .HasConversion(money => money.Amount, dbValue => new Money(dbValue));
        });

        modelBuilder.Entity<Budget>(builder =>
        {
            builder.Property(b => b.Amount)
                .HasConversion(money => money.Amount, dbValue => new Money(dbValue));
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.Property(u => u.Email)
                .HasConversion(email => email.Value, dbValue => Email.Create(dbValue));
        });
    }
}