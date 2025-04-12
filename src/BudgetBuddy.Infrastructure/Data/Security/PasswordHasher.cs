using BudgetBuddy.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BudgetBuddy.Infrastructure.Data.Security;

public class PasswordHasher : IPasswordHasher, IScopedDependency
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password) 
        => _hasher.HashPassword(null!, password);

    public bool Verify(string passwordHash, string inputPassword) 
        => _hasher.VerifyHashedPassword(null!, passwordHash, inputPassword) 
           == PasswordVerificationResult.Success;
}