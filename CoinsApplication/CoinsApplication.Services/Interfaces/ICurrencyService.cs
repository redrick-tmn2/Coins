using System.Collections.Generic;
using CoinsApplication.Models;

namespace CoinsApplication.Services.Interfaces
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyModel> GetAllCurrencies();
    }
}