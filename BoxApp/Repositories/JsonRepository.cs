namespace BoxApp.Repositories;

using BoxApp.Entities;
using System.Text.Json;
using System;

public class JsonRepository<T> : IRepository<T>
    where T : class, IEntity, new()
{
    private readonly List<T> _items = new();
    private int lastUsedId = 1;
    private readonly string path = $"{typeof(T).Name}.json";
    private readonly string auditFilePath = "audit.log";

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;
    public event EventHandler<T>? DbCleared;

    public JsonRepository()
    {
        Read();
    }

    public IEnumerable<T> GetAll()
    {
        return _items.ToList();
    }

    public T? GetById(int id)
    {
        var itemById = _items.SingleOrDefault(item => item.Id == id);
        if (itemById == null)
            Console.WriteLine($"\nObject {typeof(T).Name} with id {id} not found.");
        
        return itemById;
    }

    public void Add(T item)
    {
        if (_items.Count == 0)
        {
            item.Id = lastUsedId;
            lastUsedId++;
        }
        else if (_items.Count > 0)
        {
            lastUsedId = _items[_items.Count - 1].Id;
            item.Id = ++lastUsedId;
        }

        _items.Add(item);
        ItemAdded?.Invoke(this, item);
        LogAudit($"Added {typeof(T).Name} => {item}");
    }

    public void Remove(T item)
    {
        _items.Remove(item);
        ItemRemoved?.Invoke(this, item);
        LogAudit($"Removed {typeof(T).Name} => {item}");
    }

    public void Save()
    {
        {
            File.Delete(path);
            var json = JsonSerializer.Serialize<IEnumerable<T>>(_items);
            File.WriteAllText(path, json);
        }
    }

    public IEnumerable<T> Read()
    {
        try
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var deserializedJson = JsonSerializer.Deserialize<IEnumerable<T>>(json);
                if (deserializedJson != null)
                {
                    foreach (var item in deserializedJson)
                    {
                        _items.Add(item);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError reading file: {ex.Message}");
        }

        return _items.ToList();
    }

    public void Clear()
    {
        if (_items.Count > 0)
            _items.Clear();
        else
        {
            throw new Exception("\nFailure - the box catalog is already empty!");
        }
        lastUsedId = 1;
        DbCleared?.Invoke(this, null);
        LogAudit($"Cleared database of {typeof(T).Name}");
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