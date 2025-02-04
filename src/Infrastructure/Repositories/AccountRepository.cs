using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Data.EF;

namespace Infrastructure.Repositories;

public class AccountRepository(ApplicationDbContext context) : Repository<Account>(context), IAccountRepository, IScopedDependency
{
    
}