using BoxApp.Entities;

namespace BoxApp.Repositories;

public interface IWriteRepository<in T>
    where T : class, IEntity
{
    void Add(T item);

    void Remove(T item);

    void Save();

    void Clear();
}