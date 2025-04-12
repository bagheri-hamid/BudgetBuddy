using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Entities;
using BudgetBuddy.Infrastructure.Data.EF;

namespace BudgetBuddy.Infrastructure.Repositories;

public class AccountRepository(ApplicationDbContext context) : Repository<Account>(context), IAccountRepository, IScopedDependency
{
    
}