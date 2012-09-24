using System;
using System.Collections.Generic;

namespace DynamicLoopIoC.Domain.Repositories
{
    public interface IRepository<T>
    {
        T CreateNew();
        T Insert(T entity);
        void Save(T entity);
        void Delete(int id);
        IEnumerable<T> SearchFor(Func<T, bool> predicate);
        bool Any(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        void DeleteAll();
        T GetById(int id);
    }
}
