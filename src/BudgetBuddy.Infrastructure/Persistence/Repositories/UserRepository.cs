using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Users;
using BudgetBuddy.Infrastructure.Persistence.DbContext;

namespace BudgetBuddy.Infrastructure.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository, IScopedDependency
{
    
}