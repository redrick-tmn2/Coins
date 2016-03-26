using System;
using System.Collections.Generic;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Repositories;
using NHibernate.Linq;

namespace CoinsApplication.DAL.NHibernate.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        public IEnumerable<Currency> GetAll()
        {
            return NHibernateHelper.GetSession().Query<Currency>();
        }

        public Currency Get(int id)
        {
            return NHibernateHelper.GetSession().Get<Currency>(id);
        }

        public void Save(Currency entity)
        {
            throw new NotImplementedException();
        }
    }
}