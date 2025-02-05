using System.Linq.Expressions;

namespace Core.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, int offset = 0, int limit = 100);
    Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task SaveChangesAsync();
    Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate);
}