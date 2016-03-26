using CoinsApplication.DAL.Entities;
using FluentNHibernate.Mapping;

namespace CoinsApplication.DAL.NHibernate.Mappings
{
    public class CoinMap : ClassMap<Coin>
    {
        public CoinMap()
        {
            Id(x => x.Id);
            Map(x => x.Title).Nullable();
            Map(x => x.Year);
            
            HasMany(x => x.Images)
                .Inverse()
                .KeyColumn("CoinId")
                .Cascade.AllDeleteOrphan();

            References(x => x.Country)
                .Column("CountryId")
                .Nullable()
                .Cascade
                .SaveUpdate();

            References(x => x.Currency)
                .Column("CurrencyId")
                .Nullable()
                .Cascade
                .SaveUpdate();
        }
    }
}