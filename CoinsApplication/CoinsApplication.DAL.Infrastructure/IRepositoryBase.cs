using System.Collections.Generic;

namespace CoinsApplication.DAL.Infrastructure
{
    public interface IRepositoryBase<TEntity>
        where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(int id);
    }
}
