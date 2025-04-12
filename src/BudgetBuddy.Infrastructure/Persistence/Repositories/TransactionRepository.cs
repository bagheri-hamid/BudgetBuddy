using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Transactions;
using BudgetBuddy.Infrastructure.Persistence.DbContext;

namespace BudgetBuddy.Infrastructure.Persistence.Repositories;

public class TransactionRepository(ApplicationDbContext context) : Repository<Transaction>(context), ITransactionRepository, IScopedDependency
{
    
}