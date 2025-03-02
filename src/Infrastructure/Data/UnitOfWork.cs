using System.Collections.Concurrent;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Data.EF;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data;

public class UnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider) : IUnitOfWork, IScopedDependency
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();
    private IDbContextTransaction? _currentTransaction;
    private bool _disposed;
    
    public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        // Try to get the repository from the cache
        if (_repositories.TryGetValue(type, out var repository))
        {
            return (IRepository<TEntity>)repository;
        }

        // Create a new repository if not found in cache
        var newRepository = new Repository<TEntity>(context);
        _repositories[type] = newRepository; // Cache the repository
        return newRepository;
    }
    
    public async Task<int> CompleteAsync() => await context.SaveChangesAsync();
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = await context.Database.BeginTransactionAsync();
        return _currentTransaction;
    }
    
    public async Task CommitAsync()
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress.");
        }

        try
        {
            await context.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _currentTransaction = null;
        }
    }
    
    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose() => Dispose(true);
}