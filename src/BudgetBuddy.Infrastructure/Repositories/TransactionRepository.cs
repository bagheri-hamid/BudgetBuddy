using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Transactions;
using BudgetBuddy.Infrastructure.Data.EF;

namespace BudgetBuddy.Infrastructure.Repositories;

public class TransactionRepository(ApplicationDbContext context) : Repository<Transaction>(context), ITransactionRepository, IScopedDependency
{
    
}