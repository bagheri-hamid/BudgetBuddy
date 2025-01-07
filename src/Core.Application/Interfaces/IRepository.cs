using System.Linq.Expressions;

namespace Core.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById();
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    Task<T?> FindOne(Expression<Func<T, bool>> predicate);
    Task Add(T entity);
    Task AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task SaveChanges();
}