using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Infrastructure.Persistence.DbContext;

namespace BudgetBuddy.Infrastructure.Persistence.Repositories;

public class BudgetRepository(ApplicationDbContext context) : Repository<Budget>(context), IBudgetRepository, IScopedDependency;