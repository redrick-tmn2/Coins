using System.Collections.Generic;
using System.Linq;
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
                },
                new CountryModel
                {
                    Title = "Afghanistan",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                },
                new CountryModel
                {
                    Title = "Albania",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                },
                new CountryModel
                {
                    Title = "Algeria",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                },
                new CountryModel
                {
                    Title = "Andorra",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                },
                new CountryModel
                {
                    Title = "Argentina",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                },
                new CountryModel
                {
                    Title = "Armenia",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                },
                new CountryModel
                {
                    Title = "Aruba",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                },
                new CountryModel
                {
                    Title = "Australia",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                },
                new CountryModel
                {
                    Title = "Austria",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                },
                new CountryModel
                {
                    Title = "Azerbaijan",
                    Image = Properties.Resources.russia_64.ToImageSource(),
                }
            }.OrderBy(x => x.Title); 

        public IEnumerable<CountryModel> GetAllCountries()
        {
            return _countryModels;
        }
    }
}
