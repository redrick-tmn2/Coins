using System.Collections.Generic;
using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.DAL.Entities
{
    public class Currency : Entity<Currency>
    {
        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual IList<Coin> Coins { get; set; }
    }
}
