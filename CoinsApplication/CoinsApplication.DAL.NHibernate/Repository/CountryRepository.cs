using System;
using System.Collections.Generic;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Repositories;
using NHibernate.Linq;

namespace CoinsApplication.DAL.NHibernate.Repository
{
    public class CountryRepository : ICountryRepository
    {
        public IEnumerable<Country> GetAll()
        {
            return NHibernateHelper.GetSession().Query<Country>();
        }

        public Country Get(int id)
        {
            return NHibernateHelper.GetSession().Get<Country>(id);
        }

        public void Save(Country entity)
        {
            NHibernateHelper.GetSession().Save(entity);
        }
    }
}