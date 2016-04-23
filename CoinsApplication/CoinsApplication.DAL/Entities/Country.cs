using System;
using System.Collections.Generic;
using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.DAL.Entities
{
    public class Country : Entity<Country>, IComparable<Currency>, IComparable
    {
        public virtual string Name { get; set; }

        public virtual byte[] Flag { get; set; }

        public virtual IList<Coin> Coins { get; set; }

        public virtual int CompareTo(Currency other)
        {
            return string.Compare(Name, other?.Name, StringComparison.CurrentCulture);
        }

        public virtual int CompareTo(object obj)
        {
            return CompareTo(obj as Currency);
        }
    }
}
