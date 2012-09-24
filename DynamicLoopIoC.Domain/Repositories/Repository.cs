using System;
using System.Collections.Generic;
using System.Linq;
using DynamicLoopIoC.Common.Extensions;
using DynamicLoopIoC.Domain.Entities;

namespace DynamicLoopIoC.Domain.Repositories
{
    public abstract class Repository<T> where T : Entity
    {
        private static List<T> _dataCollection = new List<T>();
        private readonly object _lock = new object();

        public abstract T CreateNew();

        public T Insert(T entity)
        {
            lock (_lock)
            {
                var incrementedId = 1;
                if (_dataCollection.Any())
                    incrementedId = _dataCollection.Max(arg => arg.Id) + 1;
                var clone = Clone(entity);
                clone.Id = incrementedId;
                _dataCollection.Add(clone);
                return Clone(clone);
            }
        }

        public void Save(T entity)
        {
            lock (_lock)
            {
                if (_dataCollection.Any(obj => obj.Id == entity.Id))
                {
                    _dataCollection.RemoveAll(obj => obj.Id == entity.Id);
                    _dataCollection.Add(Clone(entity));
                }
            }
        }

        public void Delete(int id)
        {
            lock (_lock)
            {
                _dataCollection.RemoveAll(obj => obj.Id == id);
            }
        }

        public IEnumerable<T> SearchFor(Func<T, bool> predicate)
        {
            lock (_lock)
            {
                return _dataCollection.Where(predicate).Select(arg => Clone(arg)).OrderBy(arg => arg.Id).ToList();
            }
        }

        public bool Any(Func<T, bool> predicate)
        {
            lock (_lock)
            {
                return _dataCollection.Any(predicate);
            }
        }

        public IEnumerable<T> GetAll()
        {
            lock (_lock)
            {
                return _dataCollection.Select(arg => Clone(arg)).OrderBy(arg => arg.Id).ToList();
            }
        }

        public void DeleteAll()
        {
            lock (_lock)
            {
                _dataCollection.Clear();
            }
        }

        public T GetById(int id)
        {
            lock (_lock)
            {
                var entity = _dataCollection.SingleOrDefault(e => e.Id.Equals(id));
                if (entity == null)
                    return null;
                return Clone(entity);
            }
        }

        private T Clone(T toClone)
        {
            var newT = CreateNew();
            toClone.Clone(newT);
            return newT;
        }
    }
}