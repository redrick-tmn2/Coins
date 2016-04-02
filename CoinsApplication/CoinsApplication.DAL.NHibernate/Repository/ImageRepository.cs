using System;
using System.Collections.Generic;
using System.Linq;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Repositories;
using NHibernate.Linq;

namespace CoinsApplication.DAL.NHibernate.Repository
{
    public class ImageRepository : IImageRepository
    {
        public IEnumerable<Image> GetAll()
        {
            return NHibernateHelper.GetSession().Query<Image>().ToList();
        }

        public Image Get(int id)
        {
            return NHibernateHelper.GetSession().Get<Image>(id);
        }

        public void Save(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}