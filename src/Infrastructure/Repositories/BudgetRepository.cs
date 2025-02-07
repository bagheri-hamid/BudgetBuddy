using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Data.EF;

namespace Infrastructure.Repositories;

public class BudgetRepository(ApplicationDbContext context) : Repository<Budget>(context), IBudgetRepository, IScopedDependency;