using Core.Domain.Entities;

namespace Core.Application.Interfaces;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(string name, Guid? parentCategoryId, Guid userId);
}