using System.Collections.Generic;
using CoinsApplication.Models;
using CoinsApplication.Services.Extensions;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services.Implementation.Data
{
    public class CountryService : ICountryService
    {
        private readonly IEnumerable<CountryModel> _countryModels = new List<CountryModel>
            {
                new CountryModel
                {
                    Title = "Russia",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                },
                new CountryModel
                {
                    Title = "Saint Kitts and Nevis",
                    Image = Properties.Resources.saint_kitts_and_nevis_64.ToImageSource()
                }
            }; 

        public IEnumerable<CountryModel> GetAllCountries()
        {
            return _countryModels;
        }
    }
}
