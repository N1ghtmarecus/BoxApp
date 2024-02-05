using BoxApp.Data.Entities;

namespace BoxApp.Data.Repositories;

public interface IWriteRepository<in T>
    where T : class, IEntity
{
    void Add(T item);

    void Remove(T item);

    void Save();

    void Clear();

    void Edit(T item);
}