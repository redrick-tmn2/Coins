using CoinsApplication.DAL.Entities;
using FluentNHibernate.Mapping;

namespace CoinsApplication.DAL.NHibernate.Mappings
{
    public class CurrencyMap : ClassMap<Currency>
    {
        public CurrencyMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Code);
            HasMany(x => x.Coins)
               .KeyColumn("Id")
               .Inverse();
        }
    }
}