using Microsoft.EntityFrameworkCore;

namespace WaracleHotels.Data.Repositories;

public interface IRepositoryBase<T> where T : class
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task BulkCreateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task<T?> ReadAsync(int id, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
    Task DeleteAllAsync();
}

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly DbSet<T> _dbSet;

    public DbContext Context { get; set; }

    protected RepositoryBase(DbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        Save();
        return entity;
    }

    public virtual async Task BulkCreateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        Save();
    }

    public virtual async Task<T?> ReadAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
        Save();
    }

    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
        Save();
    }

    public virtual async Task DeleteAllAsync()
    {
        await _dbSet.ExecuteDeleteAsync(); // Doesn't need to call save, executes immediately
    }

    public virtual void Save()
    {
        Context.SaveChanges();
    }
}
