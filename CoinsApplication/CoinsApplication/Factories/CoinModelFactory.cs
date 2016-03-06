using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsApplication.Models;

namespace CoinsApplication.Factories
{
    public class CoinModelFactory
    {
        public CoinModel Create()
        {
            return new CoinModel
            {
                Title = "New coin"
            };
        }
    }
}
