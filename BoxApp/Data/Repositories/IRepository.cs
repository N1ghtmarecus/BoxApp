using BoxApp.Data.Entities;

namespace BoxApp.Data.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{
    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;
    public event EventHandler<T>? DbCleared;
}