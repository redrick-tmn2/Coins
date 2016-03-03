using System.Collections.Generic;
using CoinsApplication.Models;
using CoinsApplication.Services.Extensions;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services.Implementation.Data
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
