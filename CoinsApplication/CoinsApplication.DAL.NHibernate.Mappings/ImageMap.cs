using CoinsApplication.DAL.Entities;
using FluentNHibernate.Mapping;

namespace CoinsApplication.DAL.NHibernate.Mappings
{
    public class ImageMap : ClassMap<Image>
    {
        public ImageMap()
        {
            Id(x => x.Id);
            References(x => x.Coin, "CoinId")
                .Cascade.None();
            Map(x => x.Content);
        }
    }
}