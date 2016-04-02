using System;
using System.Collections.Generic;
using System.Linq;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Repositories;
using NHibernate.Linq;

namespace CoinsApplication.DAL.NHibernate.Repository
{
    public class CoinRepository : ICoinRepository
    {
        public IEnumerable<Coin> GetAll()
        {
            var session = NHibernateHelper.GetSession();
            return session.Query<Coin>()
                .Fetch(x => x.Country)
                .Fetch(x => x.Currency)
                .Fetch(x => x.Images)
                .ToList(); 
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
