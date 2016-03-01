using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsApplication.Models;

namespace CoinsApplication.Services.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<CountryModel> GetAllCountries();
    }
}
