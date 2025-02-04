using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Data.EF;

namespace Infrastructure.Repositories;

public class CategoryRepository(ApplicationDbContext context) : Repository<Category>(context), ICategoryRepository, IScopedDependency
{
    
}