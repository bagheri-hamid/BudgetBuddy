using System.Linq.Expressions;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : BaseEntity
{
    protected readonly DbSet<T> DbSet = context.Set<T>();
    
    /// <summary>
    /// Retrieves all entities of type T from the database.
    /// </summary>
    /// <returns>A list of all entities of type T.</returns>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    /// <summary>
    /// Retrieves an entity by its ID.
    /// Throws an exception if the entity is not found.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>The entity of type T with the specified ID.</returns>
    public async Task<T?> GetByIdAsync(Guid id)
    {
        var query = DbSet.AsQueryable();
        
        return await query.FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
    }
    
    /// <summary>
    /// Finds entities that match the given predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter entities.</param>
    /// <returns>A list of entities that match the predicate.</returns>
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.Where(predicate).ToListAsync();
    }
    
    /// <summary>
    /// Finds entities that match the given predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter entities.</param>
    /// <returns>A list of entities that match the predicate.</returns>
    public async Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate)
    {
        var query = DbSet.AsQueryable();
        query = query.Where(c => c.IsDeleted == false);
        
        return await query.Where(predicate).FirstOrDefaultAsync();
    }
    
    /// <summary>
    /// Adds a new entity of type T to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }
    
    /// <summary>
    /// Adds a collection of new entities of type T to the database.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    /// <summary>
    /// Updates an existing entity of type T in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public void Update(T entity)
    {
        DbSet.Update(entity);
    }

    /// <summary>
    /// Removes an entity of type T from the database.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }
    
    /// <summary>
    /// Removes a collection of entities of type T from the database.
    /// </summary>
    /// <param name="entities">The collection of entities to remove.</param>
    public void RemoveRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
    }
    
    /// <summary>
    /// Saves all changes made to the database context.
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Checks whether any non-deleted entity in the database set matches the specified predicate.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <param name="predicate">A function to test each entity for a condition.</param>
    /// <returns>
    /// <see langword="true"/> if any non-deleted entity in the database set matches the predicate; 
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method filters out soft-deleted entities (where <see cref="IsDeleted"/> is <see langword="true"/>)
    /// before applying the provided predicate to ensure only active entities are considered.
    /// </remarks>
    public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate)
    {
        var query = DbSet.AsQueryable();
        
        query = query.Where(c => c.IsDeleted == false);
        
        return await query.AnyAsync(predicate);
    }
}