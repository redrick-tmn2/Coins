using System.Collections.Generic;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.Repositories;

namespace CoinsApplication.ViewModel
{
    public class CountriesViewModel
    {
        public List<Country> Countries { get; } = new List<Country>();

        public CountriesViewModel(IUnitOfWorkFactory unitOfWorkFactory, ICountryRepository countryRepository)
        {
            using (unitOfWorkFactory.Create())
            {
                Countries.AddRange(countryRepository.GetAll());
            }
        }
    }
}