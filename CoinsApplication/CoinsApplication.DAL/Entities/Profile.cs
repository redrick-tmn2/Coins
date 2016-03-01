using System.Collections.Generic;

namespace CoinsApplication.DAL.Entities
{
    public class Profile
    {
        public string Name { get; set; }

        public ICollection<Coin> Coins { get; set; }
    }
}
