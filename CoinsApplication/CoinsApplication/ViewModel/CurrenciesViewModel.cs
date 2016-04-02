using System.Collections.Generic;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.Repositories;

namespace CoinsApplication.ViewModel
{
    public class CurrenciesViewModel
    {
        public List<Currency> Currencies { get; } = new List<Currency>();

        public CurrenciesViewModel(IUnitOfWorkFactory unitOfWorkFactory, ICurrencyRepository currencyRepository)
        {
            using (unitOfWorkFactory.Create())
            {
                Currencies.AddRange(currencyRepository.GetAll());
            }
        }
    }
}
