using System.Collections.Generic;
using System.Linq;
using CoinsApplication.Models;
using CoinsApplication.Services.Extensions;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services.Implementation.Data
{
    public class ProfileService : IProfileService
    {
        private readonly IEnumerable<ProfileModel> _profiles;

        public ProfileService(ICountryService countryService, ICurrencyService currencyService)
        {
            var rouble = currencyService.GetAllCurrencies().FirstOrDefault();
            var russia = countryService.GetAllCountries().FirstOrDefault();

            _profiles = new List<ProfileModel>
            {
                new ProfileModel("Профиль 1", new List<CoinModel>
                {
                    new CoinModel
                    {
                        Title = "Один рубль",
                        Image = Properties.Resources.one_rouble.ToByteArray(),
                        Country = russia,
                        Currency = rouble

                    },
                    new CoinModel
                    {
                        Title = "Два рубля",
                        Image = Properties.Resources.one_rouble.ToByteArray(),
                        Country = russia,
                        Currency = rouble
                    },
                })
            };
        }

        public IEnumerable<ProfileModel> GetAllProfiles()
        {
            return _profiles;
        }
    }
}
