using BudgetBuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace BudgetBuddy.Application.Interfaces;

/// <summary>
/// Provides a contract for unit-of-work implementations.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets a repository for the specified entity.
    /// </summary>
    IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    
    /// <summary>
    /// Saves changes to the database.
    /// </summary>
    Task<int> CompleteAsync();
    
    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionAsync();
    
    /// <summary>
    /// Commits the current transaction.
    /// </summary>
    Task CommitAsync();
    
    /// <summary>
    /// Rolls back the current transaction.
    /// </summary>
    Task RollbackTransactionAsync();
}