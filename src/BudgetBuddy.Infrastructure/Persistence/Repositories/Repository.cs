﻿using System.Linq.Expressions;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Infrastructure.Persistence.Repositories;

public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : BaseEntity
{
    protected readonly DbSet<T> DbSet = context.Set<T>();
    
    /// <summary>
    /// Retrieves all entities of type T from the database.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A list of all entities of type T.</returns>
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves an entity by its ID.
    /// Throws an exception if the entity is not found.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>The entity of type T with the specified ID.</returns>
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsQueryable();
        
        return await query.FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false, cancellationToken);
    }

    /// <summary>
    /// Asynchronously retrieves a paginated list of non-deleted entities matching the specified predicate.
    /// </summary>
    /// <param name="predicate">A condition to filter entities.</param>
    /// <param name="offset">The number of entities to skip (default: 0).</param>
    /// <param name="limit">The maximum number of entities to return (default: 100).</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing an <see cref="IEnumerable{T}"/> of matching non-deleted entities.</returns>
    /// <remarks>
    /// This method automatically filters out entities where <see cref="BaseEntity.IsDeleted"/> is <c>true</c> and applies pagination using the provided <paramref name="offset"/> and <paramref name="limit"/>.
    /// </remarks>
    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, int offset = 0, int limit = 100, CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsQueryable();
        query = query.Where(c => c.IsDeleted == false);
        
        return await query.Where(predicate).Skip(offset).Take(limit).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Asynchronously finds a single entity that matches the given predicate.
    /// Applies a filter to exclude soft-deleted entities and includes specified navigation properties.
    /// </summary>
    /// <param name="predicate">The condition to filter the entity.</param>
    /// <param name="includes">Navigation properties to include in the query.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>The first entity that matches the predicate, or null if no match is found.</returns>
    public async Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
    {
        var query = DbSet.AsQueryable();
        query = query.Where(c => c.IsDeleted == false);
        
        // Include navigation properties
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        
        return await query.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Adds a new entity of type T to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }
    
    /// <summary>
    /// Adds a collection of new entities of type T to the database.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await DbSet.AddRangeAsync(entities, cancellationToken);
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
    /// Checks whether any non-deleted entity in the database set matches the specified predicate.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <param name="predicate">A function to test each entity for a condition.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// <see langword="true"/> if any non-deleted entity in the database set matches the predicate; 
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method filters out soft-deleted entities (where <see cref="BaseEntity.IsDeleted"/> is <see langword="true"/>)
    /// before applying the provided predicate to ensure only active entities are considered.
    /// </remarks>
    public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsQueryable();
        
        query = query.Where(c => c.IsDeleted == false);
        
        return await query.AnyAsync(predicate, cancellationToken);
    }
}