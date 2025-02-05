using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Exceptions;

namespace Core.Application.Services;

public class AccountService(IAccountRepository accountRepository) : IAccountService, IScopedDependency
{
    public async Task<Account> CreateAccountAsync(string name, string type, long balance, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EmptyFiledException(nameof(Account.Name));

        if (string.IsNullOrWhiteSpace(type))
            throw new EmptyFiledException(nameof(Account.Type));
        
        var account = new Account
        {
            Name = name,
            Type = type,
            Balance = balance,
            UserId = userId,
        };
        
        await accountRepository.AddAsync(account);
        await accountRepository.SaveChangesAsync();
        
        return account;
    }

    public async Task<Account> UpdateAccountAsync(Guid accountId, string name, string type, long balance, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EmptyFiledException(nameof(Account.Name));

        if (string.IsNullOrWhiteSpace(type))
            throw new EmptyFiledException(nameof(Account.Type));
        
        if (balance < 0)
            throw new InvalidValueException(nameof(Account.Balance));

        var account = await accountRepository.GetByIdAsync(accountId);
    
        if (account == null)
            throw new ObjectNotFoundException("Account");
        
        account.Name = name;
        account.Type = type;
        account.Balance = balance;
        account.UpdatedAt = DateTime.UtcNow;
        
        await accountRepository.SaveChangesAsync();

        return account;
    }
}