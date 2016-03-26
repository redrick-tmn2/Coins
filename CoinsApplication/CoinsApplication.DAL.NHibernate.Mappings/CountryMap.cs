using CoinsApplication.DAL.Entities;
using FluentNHibernate.Mapping;

namespace CoinsApplication.DAL.NHibernate.Mappings
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Flag);

            HasMany(x => x.Coins)
               .KeyColumn("Id")
               .Inverse();
        }
    }
}
