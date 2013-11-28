namespace Proligence.Orchard.Testing.Stubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using global::Orchard.Data;

    public class RepositoryStub<T> : IRepository<T>
    {
        private readonly Func<T, int> _id;

        public RepositoryStub(Func<T, int> idResolver)
        {
            _id = idResolver;
            Data = new Dictionary<int, T>();
        }

        public IDictionary<int, T> Data { get; private set; }

        public IQueryable<T> Table
        {
            get
            {
                return Data.Values.AsQueryable();
            }
        }

        public void Create(T entity)
        {
            Data.Add(_id(entity), entity);
        }

        public void Update(T entity)
        {
            Data[_id(entity)] = entity;
        }

        public void Delete(T entity)
        {
            Data.Remove(_id(entity));
        }

        public void Copy(T source, T target)
        {
            foreach (PropertyInfo sourcePropertyInfo in typeof(T).GetProperties())
            {
                PropertyInfo destPropertyInfo = typeof(T).GetProperty(sourcePropertyInfo.Name);
                destPropertyInfo.SetValue(target, sourcePropertyInfo.GetValue(source, null), null);
            }
        }

        public void Flush()
        {
        }

        public T Get(int id)
        {
            return Data[id];
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return Data.Values.FirstOrDefault(predicate.Compile());
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return Data.Values.Count(predicate.Compile());
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate)
        {
            return Data.Values.Where(predicate.Compile());
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order)
        {
            var results = Data.Values.Where(predicate.Compile());
            var orderable = new Orderable<T>(results.AsQueryable());
            order(orderable);

            return orderable.Queryable;
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count)
        {
            var results = Data.Values.Where(predicate.Compile());
            var orderable = new Orderable<T>(results.AsQueryable());
            order(orderable);

            return orderable.Queryable.Skip(skip).Take(count);
        }
    }
}