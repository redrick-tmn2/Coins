using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsApplication.FakeServices;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services
{
    public class CurrencyService : ICurrencyService
    {
        public IEnumerable<CurrencyModel> GetAllCurrencies()
        {
            return new List<CurrencyModel>
            {
                new CurrencyModel
                {
                    Name = "Russia Rouble",
                    Image = Properties.Resources.curSymbol1088_1091_1073.ToByteArray()
                },
                new CurrencyModel
                {
                    Name = "East Caribbean Dollar",
                    Image = Properties.Resources.curSymbol36.ToByteArray()
                },
            };
        }
    }
}
