using BoxApp.Entities;

namespace BoxApp.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{ 
}