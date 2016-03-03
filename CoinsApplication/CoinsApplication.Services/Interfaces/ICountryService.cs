using System.Collections.Generic;
using CoinsApplication.Models;

namespace CoinsApplication.Services.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<CountryModel> GetAllCountries();
    }
}
