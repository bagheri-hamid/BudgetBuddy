using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Entities;
using BudgetBuddy.Infrastructure.Data.EF;

namespace BudgetBuddy.Infrastructure.Repositories;

public class CategoryRepository(ApplicationDbContext context) : Repository<Category>(context), ICategoryRepository, IScopedDependency
{
    
}