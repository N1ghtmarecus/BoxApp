﻿using BoxApp.Entities;

namespace BoxApp.Repositories.Extensions;

public static class RepositoryEntensions
{
    public static void AddBatch<T>(this IRepository<T> repository, T[] items)
        where T : class, IEntity
    {
        foreach (var item in items)
        {
            repository.Add(item);
        }

        repository.Save();
    }
}