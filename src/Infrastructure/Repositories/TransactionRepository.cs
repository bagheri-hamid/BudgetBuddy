using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Data.EF;

namespace Infrastructure.Repositories;

public class TransactionRepository(ApplicationDbContext context) : Repository<Transaction>(context), ITransactionRepository, IScopedDependency
{
    
}