using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Infrastructure.Data.EF;

namespace BudgetBuddy.Infrastructure.Repositories;

public class AccountRepository(ApplicationDbContext context) : Repository<Account>(context), IAccountRepository, IScopedDependency
{
    
}