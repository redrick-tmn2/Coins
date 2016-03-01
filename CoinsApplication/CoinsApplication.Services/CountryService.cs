using System.Collections.Generic;
using CoinsApplication.FakeServices;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services
{
    public class CountryService : ICountryService
    {
        public IEnumerable<CountryModel> GetAllCountries()
        {
            return new List<CountryModel>
            {
                new CountryModel
                {
                    Name = "Russia",
                    Image = Properties.Resources.russia_64.ToByteArray(),
                },
                new CountryModel
                {
                    Name = "Saint Kitts and Nevis",
                    Image = Properties.Resources.saint_kitts_and_nevis_64.ToByteArray()
                }
            };
        }
    }
}
