using System.Collections.Generic;

namespace CoinsApplication.DAL.Infrastructure
{
    public interface IRepositoryBase<TEntity>
        where TEntity : Entity<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(int id);

        void Save(TEntity entity);
    }
}
