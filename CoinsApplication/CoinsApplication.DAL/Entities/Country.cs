using System.Collections.Generic;
using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.DAL.Entities
{
    public class Country : Entity<Country>
    {
        public virtual string Name { get; set; }

        public virtual byte[] Flag { get; set; }

        public virtual IList<Coin> Coins { get; set; }
    }
}
