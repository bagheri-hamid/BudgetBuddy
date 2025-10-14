using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Application.UseCases.Accounts.CreateAccount;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Exceptions;
using BudgetBuddy.Domain.ValueObjects;
using Moq;

namespace BudgetBuddy.Application.Tests.UseCases.Accounts;

public class CreateAccountHandlerTests
{
    private static Money MoneyOf(long amount) => new(amount);

    [Fact]
    public async Task Handle_ValidRequest_AddAccount_CompleteAndReturnsAccount()
    {
        // Arrange
        var mockRepo = new Mock<IAccountRepository>();
        var mockTokenHelper = new Mock<ITokenHelper>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        
        var userId = Guid.NewGuid();
        mockTokenHelper.Setup(t => t.GetUserId()).Returns(userId);
        
        Account? capturedAccount = null;
        mockRepo
            .Setup(r => r.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Callback<Account, CancellationToken>((acct, _) => capturedAccount = acct)
            .Returns(Task.CompletedTask);
        
        mockUnitOfWork
            .Setup(u => u.CompleteAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(0));
        
        var handler = new CreateAccountHandler(mockRepo.Object, mockTokenHelper.Object, mockUnitOfWork.Object);
        
        const string name = "My Account";
        const string type = "Checking";
        const long balance = 12345L;
        var command = new CreateAccountCommand(name, type, balance);

        var ct = new CancellationTokenSource().Token;
        
        // Act
        var result = await handler.Handle(command, ct);

        // Assert - returned entity
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
        Assert.Equal(type, result.Type);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(MoneyOf(balance), result.Balance);
        
        // Assert - repository/unit-of-work interactions with same CancellationToken
        mockRepo.Verify(r => r.AddAsync(
                It.Is<Account>(a =>
                    a.Name == name &&
                    a.Type == type &&
                    a.UserId == userId &&
                    a.Balance == MoneyOf(balance)
                ),
                ct),
            Times.Once);

        mockUnitOfWork.Verify(u => u.CompleteAsync(ct), Times.Once);
        
        // Assert token helper called once
        mockTokenHelper.Verify(t => t.GetUserId(), Times.Once);

        // Assert capturedAccount
        Assert.NotNull(capturedAccount);
        Assert.Equal(name, capturedAccount!.Name);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Handle_InvalidName_ThrowsEmptyFiledException_AndDoesNotCallRepoOrUow(string invalidName)
    {
        // Arrange
        var mockRepo = new Mock<IAccountRepository>();
        var mockTokenHelper = new Mock<ITokenHelper>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        var handler = new CreateAccountHandler(mockRepo.Object, mockTokenHelper.Object, mockUnitOfWork.Object);

        var command = new CreateAccountCommand(invalidName, "Type", 0L);

        // Act & Assert
        await Assert.ThrowsAsync<EmptyFiledException>(() => handler.Handle(command, CancellationToken.None));

        mockRepo.Verify(r => r.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Never);
        mockUnitOfWork.Verify(u => u.CompleteAsync(It.IsAny<CancellationToken>()), Times.Never);
        mockTokenHelper.Verify(t => t.GetUserId(), Times.Never);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Handle_InvalidType_ThrowsEmptyFiledException_AndDoesNotCallRepoOrUow(string invalidType)
    {
        // Arrange
        var mockRepo = new Mock<IAccountRepository>();
        var mockTokenHelper = new Mock<ITokenHelper>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        var handler = new CreateAccountHandler(mockRepo.Object, mockTokenHelper.Object, mockUnitOfWork.Object);

        var command = new CreateAccountCommand("Name", invalidType, 0L);

        // Act & Assert
        await Assert.ThrowsAsync<EmptyFiledException>(() => handler.Handle(command, CancellationToken.None));

        mockRepo.Verify(r => r.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Never);
        mockUnitOfWork.Verify(u => u.CompleteAsync(It.IsAny<CancellationToken>()), Times.Never);
        mockTokenHelper.Verify(t => t.GetUserId(), Times.Never);
    }
    
    [Fact]
    public async Task Handle_WhenCancellationTokenProvided_PassesSameTokenToRepositoryAndUnitOfWork()
    {
        // Arrange
        var mockRepo = new Mock<IAccountRepository>();
        var mockTokenHelper = new Mock<ITokenHelper>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        var userId = Guid.NewGuid();
        mockTokenHelper.Setup(t => t.GetUserId()).Returns(userId);

        CancellationToken? repoToken = null;
        mockRepo
            .Setup(r => r.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Callback<Account, CancellationToken>((_, t) => repoToken = t)
            .Returns(Task.CompletedTask);

        CancellationToken? uowToken = null;
        mockUnitOfWork
            .Setup(u => u.CompleteAsync(It.IsAny<CancellationToken>()))
            .Callback<CancellationToken>(t => uowToken = t)
            .Returns(Task.FromResult(0));

        var handler = new CreateAccountHandler(mockRepo.Object, mockTokenHelper.Object, mockUnitOfWork.Object);

        var command = new CreateAccountCommand("Name", "Type", 1L);
        using var cts = new CancellationTokenSource();
        var token = cts.Token;

        // Act
        _ = await handler.Handle(command, token);

        // Assert - the same token was passed to dependencies
        Assert.Equal(token, repoToken);
        Assert.Equal(token, uowToken);
    }
}