using System.Collections.Generic;
using System.Linq;
using CoinsApplication.Models;
using CoinsApplication.Services.Extensions;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services.Implementation.Data
{
    public class CoinService : ICoinService
    {
        private readonly IEnumerable<CoinModel> _coins;

        public CoinService(ICountryService countryService, ICurrencyService currencyService)
        {
            var allCurrencies = currencyService.GetAllCurrencies().ToList();
            var allCountries = countryService.GetAllCountries().ToList();

            var rouble = allCurrencies.FirstOrDefault();
            var russia = allCountries.FirstOrDefault();

            var dollar = allCurrencies.LastOrDefault();
            var stKits = allCountries.LastOrDefault();

            _coins = new List<CoinModel>
            {
                new CoinModel
                {
                    Title = "Один рубль",
                    Image = Properties.Resources.one_rouble.ToImageSource(),
                    Country = russia,
                    Currency = rouble

                },
                new CoinModel
                {
                    Title = "Два рубля",
                    Image = Properties.Resources.one_rouble.ToImageSource(),
                    Country = russia,
                    Currency = rouble
                },
                new CoinModel
                {
                    Title = "Один каррибский доллар",
                    Image = Properties.Resources.one_rouble.ToImageSource(),
                    Country = stKits,
                    Currency = dollar
                }
            };
        }

        public IEnumerable<CoinModel> GetAllCoins()
        {
            return _coins;
        }
    }
}
