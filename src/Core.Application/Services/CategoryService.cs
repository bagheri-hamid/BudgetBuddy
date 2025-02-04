using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Exceptions;

namespace Core.Application.Services;

public class CategoryService(IRepository<Category> categoryRepository) : ICategoryService, IScopedDependency
{
    public async Task<Category> CreateCategoryAsync(string name, Guid? parentCategoryId, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EmptyNameException();

        if (parentCategoryId != null)
        {
            var isExist = await categoryRepository.IsExistsAsync(c => c.Id == parentCategoryId);

            if (!isExist)
                throw new ParentCategoryNotFoundException();
        }

        var category = new Category
        {
            Name = name,
            ParentCategoryId = parentCategoryId,
            UserId = userId,
        };
        
        await categoryRepository.AddAsync(category);
        await categoryRepository.SaveChangesAsync();
        
        return category;
    }
}