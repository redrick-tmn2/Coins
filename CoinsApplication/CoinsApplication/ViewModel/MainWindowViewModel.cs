using System.Collections.ObjectModel;
using System.Linq;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.ViewModel.Factories;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ProfileViewModel> Profiles { get; } = new ObservableCollection<ProfileViewModel>();

        private ProfileViewModel _selectedProfile;
        public ProfileViewModel SelectedProfile
        {
            get { return _selectedProfile; }
            set { Set(ref _selectedProfile, value); }
        }

        public ObservableCollection<CountryModel> Countries { get; } = new ObservableCollection<CountryModel>();

        public ObservableCollection<CurrencyModel> Currencies { get; } = new ObservableCollection<CurrencyModel>();

        public MainWindowViewModel(IProfileService profileService, 
            ICountryService countryService, 
            ICurrencyService currencyService, 
            ProfileViewModelFactory profileViewModelFactory)
        {
            RefreshCountries(countryService);
            RefreshCurrencies(currencyService);
            RefreshProfiles(profileService, profileViewModelFactory);
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

        private void RefreshProfiles(IProfileService profileService, ProfileViewModelFactory profileViewModelFactory)
        {
            var allProfiles = profileService.GetAllProfiles();

            Profiles.Clear();

            foreach (var profile in allProfiles)
            {
                var profileViewModel = profileViewModelFactory.Create(profile);
                Profiles.Add(profileViewModel);
            }

            SelectedProfile = Profiles.FirstOrDefault();
        }
    }   
}
