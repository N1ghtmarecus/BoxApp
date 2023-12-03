namespace BoxApp.Repositories;

using BoxApp.Entities;
using Microsoft.EntityFrameworkCore;

public class ListRepository<T> : IRepository<T>
    where T : class, IEntity, new()
{
    private readonly List<T> _items = new();

    public IEnumerable<T> GetAll()
    {
        return _items.ToList();
    }

    public T GetById(int id)
    {
        return _items.Single(item => item.Id == id);
    }

    public void Add(T item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
    }

    public void Remove(T item)
    {
        _items.Remove(item);
    }

    public void Save()
    {
        // save is not required with List
    }

    public IEnumerable<T> Read()
    {
        return _items.ToList();
    }

    public void Clear()
    {
        _items.Clear();
    }

}