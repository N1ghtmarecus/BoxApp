using BoxApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxApp.Data.Repositories;

public class SqlRepository<T> : IRepository<T>
    where T : class, IEntity, new()
{
    private readonly BoxAppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    private readonly string auditFilePath = "audit.log";

    public SqlRepository(BoxAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;
    public event EventHandler<T>? ItemUpdated;
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
        ItemAdded?.Invoke(this, item);
        LogAudit($"\nAdded {typeof(T).Name} => {item}\n");
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
        ItemRemoved?.Invoke(this, item);
        LogAudit($"\nRemoved {typeof(T).Name} => {item}\n");
    }

    public void Edit(T item)
    {
        ItemUpdated?.Invoke(this, item);
        LogAudit($"\nUpdated {typeof(T).Name} => {item}\n");
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public IEnumerable<T> Read()
    {
        return _dbSet.ToList();
    }

    public void Clear()
    {
        _dbSet.RemoveRange(_dbSet);
        DbCleared?.Invoke(this, null!);
        LogAudit($"\nCleared database of {typeof(T).Name}\n");
    }

    private void LogAudit(string logEntry)
    {
        try
        {
            File.AppendAllText(auditFilePath, $"[{DateTime.Now:yyyy-MM-ddTHH:mm:ss}]-{logEntry}" + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to audit file: {ex.Message}");
        }
    }
}