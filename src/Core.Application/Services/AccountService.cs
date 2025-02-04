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
}