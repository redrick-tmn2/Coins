using System;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.Repositories;

namespace CoinsApplication.DAL.NHibernate.Repository
{
    public class SaveObjectRepository : ISaveObjectRepository
    {
        public void Remove(IEntity entity)
        {
            NHibernateHelper.GetSession().Delete(entity);
        }

        public void Save(IEntity entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = (int) NHibernateHelper.GetSession().Save(entity);
            }
            else
            {
                NHibernateHelper.GetSession().Update(entity);
            }
        }
    }
}
