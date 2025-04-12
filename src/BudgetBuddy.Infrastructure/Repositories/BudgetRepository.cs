using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Infrastructure.Data.EF;

namespace BudgetBuddy.Infrastructure.Repositories;

public class BudgetRepository(ApplicationDbContext context) : Repository<Budget>(context), IBudgetRepository, IScopedDependency;