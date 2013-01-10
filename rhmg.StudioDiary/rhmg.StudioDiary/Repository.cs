using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Raven.Client;
using Raven.Client.Linq;

namespace rhmg.StudioDiary
{
    public interface IRepository<T> where T : Entity
    {
        T Get(string id);
        List<T> Get(Expression<Func<T, bool>> matching);
        T Put(T entity);
        bool Exists(Expression<Func<T, bool>> compareOn);
        List<T> GetLazily(Expression<Func<T, bool>> compareOn);
        List<Z> IndexGet<Z>(string indexName);
    }

    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly IDocumentSession _session;
        public Repository(IDocumentSession session)
        {
            _session = session;
        }
        
        public T Get(string id)
        {
            return _session.Load<T>(id);
        }

        public List<T> Get(Expression<Func<T, bool>> matching)
        {
            return _session.Query<T>().Where(matching).ToList();
        }

        public T Put(T entity)
        {
            _session.Store(entity);
            _session.SaveChanges();
            return entity;
        }
        public bool Exists(Expression<Func<T, bool>> compareOn)
        {
            return Get(compareOn).Any();
        }

        public List<T> GetLazily(Expression<Func<T, bool>> compareOn)
        {
            return _session.Query<T>().Where(compareOn).Lazily().Value.ToList();
        }

        public List<Z> IndexGet<Z>(string indexName)
        {
            return _session.Query<Z>(indexName).ToList();
        }
    }
}