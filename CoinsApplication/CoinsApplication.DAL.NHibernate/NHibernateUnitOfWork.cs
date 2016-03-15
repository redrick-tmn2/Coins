using System;
using System.Data;
using CoinsApplication.DAL.Infrastructure;
using NHibernate;
using NHibernate.Context;

namespace CoinsApplication.DAL.NHibernate
{
    internal class NHibernateUnitOfWork : IUnitOfWork
    {
        #region private fields

        private readonly ISession _session;
        private ITransaction _transaction;

        #endregion

        #region ctor

        public NHibernateUnitOfWork(ISession session, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (!CurrentSessionContext.HasBind(session.SessionFactory))
            {
                CurrentSessionContext.Bind(session);
            }

            _session = session;
            _transaction = session.BeginTransaction(isolationLevel);
        }

        #endregion

        #region IUnitOfWork Members

        public void Dispose()
        {
            if (!_transaction.WasCommitted && !_transaction.WasRolledBack)
            {
                _transaction.Rollback();
            }

            _transaction.Dispose();
            _transaction = null;

            CurrentSessionContext.Unbind(_session.SessionFactory);
            _session.Dispose();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        #endregion
    }
}