using System.Collections.Generic;
using CoinsApplication.FakeServices;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services
{
    public class CountryService : ICountryService
    {
        private readonly IEnumerable<CountryModel> _countryModels = new List<CountryModel>
            {
                new CountryModel
                {
                    Title = "Russia",
                    Image = Properties.Resources.russia_64.ToByteArray(),
                },
                new CountryModel
                {
                    Title = "Saint Kitts and Nevis",
                    Image = Properties.Resources.saint_kitts_and_nevis_64.ToByteArray()
                }
            }; 

        public IEnumerable<CountryModel> GetAllCountries()
        {
            return _countryModels;
        }
    }
}
