using System;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Context;

namespace CoinsApplication.DAL.NHibernate
{
    internal static class NHibernateHelper
    {
        private static readonly object _lockObject = new object();

        private static FluentConfiguration _configuration;
        private static ISessionFactory _sessionFactory;

        internal static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    lock (_lockObject)
                    {
                        if (_sessionFactory == null)
                            _sessionFactory = Configuration.BuildSessionFactory();
                    }
                }

                return _sessionFactory;
            }
        }

        private static FluentConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    lock (_lockObject)
                    {
                        if (_configuration == null)
                        {
                            _configuration = NHibernateConfigurationFactory.GetConfiguration();
                        }
                    }
                }

                return _configuration;
            }
        }

        internal static ISession GetSession()
        {
            if (CurrentSessionContext.HasBind(SessionFactory))
            {
                return SessionFactory.GetCurrentSession();
            }

            throw new InvalidOperationException("Database access logic cannot be used, if session not opened. Implicitly session usage not allowed now. Please open session explicitly through UnitOfWorkFactory.StartLongConversation method");
        }
    }
}
