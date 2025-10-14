using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.Transactions;
using BudgetBuddy.Domain.ValueObjects;

namespace BudgetBuddy.Domain.Tests.Accounts;

public class AccountTests
{
    // ------------------------- Helpers --------------------------------
    private static Money CreateMoney(long amount) => new(amount);
    
    private static Transaction CreateTransaction(Money money, string description, TransactionType type) =>
        new(money, description, type, DateTime.Now, categoryId: Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
    // ------------------------------------------------------------------

    [Fact]
    public void Ctor_WithValidParameters_SetsProperties()
    {
        // Arrange
        const string name = "Budget";
        const string type = "Buddy";
        var balance = CreateMoney(100L);
        var userId = Guid.NewGuid();

        // Act
        var account = new Account(name, type, balance, userId);

        // Assert
        Assert.Equal(name, account.Name);
        Assert.Equal(type, account.Type);
        Assert.Equal(balance, account.Balance);
        Assert.Equal(userId, account.UserId);
        Assert.False(account.IsDeleted);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Ctor_WithInvalidName_ThrowsEmptyFieldException(string? invalidName)
    {
        // Arrange
        const string type = "Budget Buddy";
        var balance = CreateMoney(1000L);
        var userId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<EmptyFiledException>(() => new Account(invalidName!, type, balance, userId));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Ctor_WithInvalidType_ThrowsEmptyFieldException(string? invalidType)
    {
        // Arrange
        const string name = "Budget Buddy";
        var balance = CreateMoney(1000L);
        var userId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<EmptyFiledException>(() => new Account(name, invalidType!, balance, userId));
    }

    [Fact]
    public void Ctor_WithNegativeBalance_ThrowsInvalidValueException()
    {
        // Arrange
        const string name = "Name";
        const string type = "Type";
        var negativeBalance = CreateMoney(-1L);
        var userId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<InvalidValueException>(() => new Account(name, type, negativeBalance, userId));
    }

    [Fact]
    public void Update_WithValidValues_UpdatesPropertiesAndUpdatedAt()
    {
        // Arrange
        var account = new Account("A", "T", CreateMoney(10L), Guid.NewGuid());
        var oldUpdatedAt = account.UpdatedAt;
        Thread.Sleep(1); // ensure time difference if UpdatedAt was default

        // Act
        account.Update("NewName", "NewType", CreateMoney(50L));

        // Assert
        Assert.Equal("NewName", account.Name);
        Assert.Equal("NewType", account.Type);
        Assert.Equal(CreateMoney(50L), account.Balance);
        Assert.NotEqual(oldUpdatedAt, account.UpdatedAt);
        Assert.True(account.UpdatedAt > DateTime.MinValue);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Update_WithInvalidName_ThrowsEmptyFiledException(string? invalidName)
    {
        // Arrange
        var account = new Account("Name", "Type", CreateMoney(0L), Guid.NewGuid());

        // Act & Assert
        Assert.Throws<EmptyFiledException>(() => account.Update(invalidName!, "Type", CreateMoney(0L)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Update_WithInvalidType_ThrowsEmptyFiledException(string? invalidType)
    {
        // Arrange
        var account = new Account("Name", "Type", CreateMoney(0L), Guid.NewGuid());

        // Act & Assert
        Assert.Throws<EmptyFiledException>(() => account.Update("Name", invalidType!, CreateMoney(0L)));
    }

    [Fact]
    public void UpdateBalance_Income_IncreasesBalanceAndSetsUpdatedAt()
    {
        // Arrange
        var account = new Account("A", "T", CreateMoney(100L), Guid.NewGuid());
        var amount = CreateMoney(25L);
        var before = account.Balance;
        var oldUpdatedAt = account.UpdatedAt;

        // Act
        account.UpdateBalance(amount, TransactionType.Income);

        // Assert
        Assert.Equal(before + amount, account.Balance);
        Assert.NotEqual(oldUpdatedAt, account.UpdatedAt);
    }

    [Fact]
    public void UpdateBalance_Expense_DecreasesBalanceAndSetsUpdatedAt()
    {
        // Arrange
        var account = new Account("A", "T", CreateMoney(100L), Guid.NewGuid());
        var amount = CreateMoney(40L);
        var before = account.Balance;
        var oldUpdatedAt = account.UpdatedAt;

        // Act
        account.UpdateBalance(amount, TransactionType.Expense);

        // Assert
        Assert.Equal(before - amount, account.Balance);
        Assert.NotEqual(oldUpdatedAt, account.UpdatedAt);
    }

    [Fact]
    public void Delete_MarksAsDeletedAndSetsUpdatedAt()
    {
        // Arrange
        var account = new Account("A", "T", CreateMoney(0L), Guid.NewGuid());

        // Act
        account.Delete();

        // Assert
        Assert.True(account.IsDeleted);
        Assert.True(account.UpdatedAt > DateTime.MinValue);
    }

    [Fact]
    public void RecalculateBalance_Income_RecalculatesCorrectly()
    {
        // Arrange
        var account = new Account("A", "T", CreateMoney(100L), Guid.NewGuid());
        var oldTxAmount = CreateMoney(30L);
        var newTxAmount = CreateMoney(50L);

        // Act
        account.RecalculateBalance(TransactionType.Income, oldTxAmount, newTxAmount);

        // Expected: (100 - old) + new = (100 - 30) + 50 = 120
        Assert.Equal(CreateMoney(120L), account.Balance);
        Assert.True(account.UpdatedAt > DateTime.MinValue);
    }
    
    [Fact]
    public void RecalculateBalance_Expense_RecalculatesCorrectly()
    {
        // Arrange
        var account = new Account("A", "T", CreateMoney(100L), Guid.NewGuid());
        var oldTxAmount = CreateMoney(30L);
        var newTxAmount = CreateMoney(50L);

        // Act
        account.RecalculateBalance(TransactionType.Expense, oldTxAmount, newTxAmount);

        // Expected: (100 + old) - new = (100 + 30) - 50 = 80
        Assert.Equal(CreateMoney(80L), account.Balance);
        Assert.True(account.UpdatedAt > DateTime.MinValue);
    }
    
    [Fact]
    public void RevertTransaction_Income_SubtractsTransactionAmount()
    {
        // Arrange
        var account = new Account("A", "T", CreateMoney(200L), Guid.NewGuid());
        var tx = CreateTransaction(CreateMoney(50L), "description", TransactionType.Income);

        // Act
        account.RevertTransaction(tx);

        // Assert: income was reverted by subtraction
        Assert.Equal(CreateMoney(150L), account.Balance);
        Assert.True(account.UpdatedAt > DateTime.MinValue);
    }
    
    [Fact]
    public void RevertTransaction_Expense_AddsTransactionAmount()
    {
        // Arrange
        var account = new Account("A", "T", CreateMoney(200L), Guid.NewGuid());
        var tx = CreateTransaction(CreateMoney(50L), "description", TransactionType.Expense);

        // Act
        account.RevertTransaction(tx);

        // Assert: expense was reverted by addition
        Assert.Equal(CreateMoney(250L), account.Balance);
        Assert.True(account.UpdatedAt > DateTime.MinValue);
    }
}