using System.Collections.Generic;
using CoinsApplication.FakeServices;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IEnumerable<CurrencyModel> _currencyModels = new List<CurrencyModel>
            {
                new CurrencyModel
                {
                    Title = "Russia Rouble",
                    Image = Properties.Resources.curSymbol1088_1091_1073.ToByteArray()
                },
                new CurrencyModel
                {
                    Title = "East Caribbean Dollar",
                    Image = Properties.Resources.curSymbol36.ToByteArray()
                },
            }; 

        public IEnumerable<CurrencyModel> GetAllCurrencies()
        {
            return _currencyModels;
        }
    }
}
