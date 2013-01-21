using System.Collections.Generic;

namespace Calendar.Domain.Common
{
    public interface IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct
    {
        void SaveOrUpdate(TEntity obj);
        void Save(TEntity obj);
        void Update(TEntity obj);
        TEntity GetById(TId id);
        TEntity LoadById(TId id);
        IList<TEntity> GetAll();
        IList<TEntity> GetByIds(IList<TId> ids);
        void Flush();
    }
}