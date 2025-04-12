using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Infrastructure.Persistence.DbContext;

namespace BudgetBuddy.Infrastructure.Persistence.Repositories;

public class AccountRepository(ApplicationDbContext context) : Repository<Account>(context), IAccountRepository, IScopedDependency
{
    
}