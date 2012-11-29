using System.Collections.Generic;
using System.Linq;
using NHibernate.ByteCode.Castle;
using NHibernate.Criterion;
using Sales.Domain.Common;

namespace Sales.Data.Common
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct
    {
        protected readonly ISessionProvider _sessionProvider;

        protected Repository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        private static ProxyFactoryFactory _pff;

        public virtual void SaveOrUpdate(TEntity obj)
        {
            _sessionProvider.GetCurrent().SaveOrUpdate(obj);
        }

        public virtual void Save(TEntity obj)
        {
            _sessionProvider.GetCurrent().Save(obj);
        }

        public virtual void Update(TEntity obj)
        {
            _sessionProvider.GetCurrent().Update(obj);
        }

        public virtual TEntity GetById(TId id)
        {
            return _sessionProvider.GetCurrent().Get<TEntity>(id);
        }

        public virtual TEntity LoadById(TId id)
        {
            return _sessionProvider.GetCurrent().Load<TEntity>(id);
        }

        public virtual List<TEntity> GetAll()
        {
            var criteriaQuery = _sessionProvider.GetCurrent().CreateCriteria(typeof(TEntity));
            return (List<TEntity>)criteriaQuery.List<TEntity>();
        }

        public List<TEntity> GetByIds(List<TId> ids)
        {
            return _sessionProvider.GetCurrent().CreateCriteria<TEntity>()
                .Add(Expression.In("Id", ids))
                .List<TEntity>()
                .ToList();
        }

        public void Flush()
        {
            _sessionProvider.GetCurrent().Flush();
        }
    }
}

