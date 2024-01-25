using BoxApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxApp.Repositories;

public class SqlRepository<T> : IRepository<T>
    where T : class, IEntity, new()
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    private readonly Action<T>? _itemAddedCallback;

    public SqlRepository(DbContext dbContext, Action<T>? itemAddedCallback = null)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
        _itemAddedCallback = itemAddedCallback;
    }

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;
    public event EventHandler<T>? DbCleared;

    public IEnumerable<T> GetAll()
    {
        return _dbSet.OrderBy(item => item.Id).ToList();
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Add(T item)
    {
        _dbSet.Add(item);
        _itemAddedCallback?.Invoke(item);
        ItemAdded?.Invoke(this, item);
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public IEnumerable<T> Read() 
    {
        return _dbSet.ToList();
    }

    public void Clear() {}
}