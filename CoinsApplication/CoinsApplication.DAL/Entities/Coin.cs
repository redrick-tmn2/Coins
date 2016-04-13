using System.Collections.Generic;
using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.DAL.Entities
{
    public class Coin : Entity<Coin>
    {
        public virtual string Title { get; set; }

        public virtual int? Year { get; set; }

        public virtual double? Diameter { get; set; }

        public virtual double? Thickness { get; set; }

        public virtual IList<Image> Images { get; set; }

        public virtual Country Country { get; set; }

        public virtual Currency Currency { get; set; }
    }
}