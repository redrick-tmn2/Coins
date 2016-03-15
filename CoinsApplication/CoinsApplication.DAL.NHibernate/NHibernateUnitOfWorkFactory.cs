using System.Data;
using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.DAL.NHibernate
{
    public class NHibernateUnitOfWorkFactory : IUnitOfWorkFactory
    {
        #region IUnitOfWorkFactory Members

        public IUnitOfWork Create(IsolationLevel isolationLevel)
        {
            return new NHibernateUnitOfWork(NHibernateHelper.SessionFactory.OpenSession(), isolationLevel);
        }

        public IUnitOfWork Create()
        {
            return Create(IsolationLevel.ReadCommitted);
        }

        #endregion
    }
}