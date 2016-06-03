using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Orchard.Data;

namespace Proligence.Orchard.Testing.Stubs
{
    public class RepositoryStub<T> : IRepository<T>
    {
        private readonly Func<T, int> _identityAccessor;
        private readonly IDictionary<int, T> _table;

        public RepositoryStub(Func<T, int> identityAccessor)
        {
            _identityAccessor = identityAccessor;
            _table = new ConcurrentDictionary<int, T>();
        }

        public void Create(T entity)
        {
            _table.Add(_identityAccessor(entity), entity);
        }

        public void Update(T entity)
        {
            var id = _identityAccessor(entity);

            if (_table.ContainsKey(id))
            {
                _table[id] = entity;
            }
        }

        public void Delete(T entity)
        {
            _table.Remove(_identityAccessor(entity));
        }

        public void Copy(T source, T target)
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
        }

        public T Get(int id)
        {
            T entity;
            return _table.TryGetValue(id, out entity) ? entity : default(T);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _table.Values.AsQueryable().Where(predicate).FirstOrDefault();
        }

        public IQueryable<T> Table
        {
            get
            {
                return _table.Values.AsQueryable();
            }
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _table.Values.AsQueryable().Where(predicate).Count();
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate)
        {
            return _table.Values.AsQueryable().Where(predicate).ToArray();
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order)
        {
            var results = _table.Values.AsQueryable().Where(predicate).AsQueryable();
            var ordered = new Orderable<T>(results);
            order(ordered);

            return ordered.Queryable;
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count)
        {
            var results = _table.Values.AsQueryable().Where(predicate).AsQueryable();
            var ordered = new Orderable<T>(results);
            order(ordered);

            return ordered.Queryable.Skip(skip).Take(count);
        }
    }
}