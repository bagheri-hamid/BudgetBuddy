using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Data.EF;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository, IScopedDependency
{
    
}