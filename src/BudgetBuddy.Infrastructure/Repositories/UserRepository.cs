using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Entities;
using BudgetBuddy.Infrastructure.Data.EF;

namespace BudgetBuddy.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository, IScopedDependency
{
    
}