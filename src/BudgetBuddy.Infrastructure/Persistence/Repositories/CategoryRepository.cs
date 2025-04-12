using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Categories;
using BudgetBuddy.Infrastructure.Persistence.DbContext;

namespace BudgetBuddy.Infrastructure.Persistence.Repositories;

public class CategoryRepository(ApplicationDbContext context) : Repository<Category>(context), ICategoryRepository, IScopedDependency
{
    
}