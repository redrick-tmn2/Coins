using System.Collections.ObjectModel;
using System.Linq;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ProfileViewModel> _profiles = new ObservableCollection<ProfileViewModel>();
        public ObservableCollection<ProfileViewModel> Profiles
        {
            get { return _profiles; }
        }

        private ProfileViewModel _selectedProfile;
        public ProfileViewModel SelectedProfile
        {
            get { return _selectedProfile; }
            set { Set(ref _selectedProfile, value); }
        }

        private readonly ObservableCollection<CountryModel> _countries = new ObservableCollection<CountryModel>();
        public ObservableCollection<CountryModel> Countries
        {
            get { return _countries; }
        }

        private readonly ObservableCollection<CurrencyModel> _currencies = new ObservableCollection<CurrencyModel>();
        public ObservableCollection<CurrencyModel> Currencies
        {
            get { return _currencies; }
        }

        public MainWindowViewModel(IProfileService profileService, ICountryService countryService, ICurrencyService currencyService)
        {
            RefreshCountries(countryService);
            RefreshCurrencies(currencyService);
            RefreshProfiles(profileService);
        }

        private void RefreshCurrencies(ICurrencyService currencyService)
        {
            var allCurrencies = currencyService.GetAllCurrencies();

            Currencies.Clear();

            foreach (var currency in allCurrencies)
            {
                Currencies.Add(currency);
            }
        }

        private void RefreshCountries(ICountryService countryService)
        {
            var allCountries = countryService.GetAllCountries();

            Countries.Clear();

            foreach (var country in allCountries)
            {
                Countries.Add(country);
            }
        }

        private void RefreshProfiles(IProfileService profileService)
        {
            var allProfiles = profileService.GetAllProfiles();

            Profiles.Clear();

            foreach (var profile in allProfiles)
            {
                Profiles.Add(new ProfileViewModel(profile));
            }

            SelectedProfile = Profiles.FirstOrDefault();
        }
    }   
}
