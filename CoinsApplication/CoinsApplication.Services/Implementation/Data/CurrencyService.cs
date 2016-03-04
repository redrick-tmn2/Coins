using System.Collections.Generic;
using CoinsApplication.Models;
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
                    Code = "RUB"
                },
                new CurrencyModel
                {
                    Title = "East Caribbean Dollar",
                    Code = "XCD"
                },
            }; 

        public IEnumerable<CurrencyModel> GetAllCurrencies()
        {
            return _currencyModels;
        }
    }
}
