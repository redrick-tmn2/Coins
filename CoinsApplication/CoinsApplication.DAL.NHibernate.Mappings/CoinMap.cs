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
            Map(x => x.Image).Nullable();
            Map(x => x.Year);

            References(x => x.Country)
                .Column("CountryId");
            References(x => x.Currency)
                .Column("CurrencyId");
        }
    }
}