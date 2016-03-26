using System;
using System.Collections.Generic;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Repositories;
using NHibernate.Linq;

namespace CoinsApplication.DAL.NHibernate.Repository
{
    public class CoinRepository : ICoinRepository
    {
        public IEnumerable<Coin> GetAll()
        {
            return NHibernateHelper.GetSession().Query<Coin>();
        }

        public Coin Get(int id)
        {
            return NHibernateHelper.GetSession().Get<Coin>(id);
        }

        public void Save(Coin entity)
        {
            throw new NotImplementedException();
        }
    }
}
