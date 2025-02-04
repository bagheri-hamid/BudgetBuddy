using Core.Domain.Entities;

namespace Core.Application.Interfaces;

public interface IAccountService
{
    Task<Account> CreateAccountAsync(string name, string type, long balance, Guid userId);
}