using System.Collections.ObjectModel;
using System.Linq;
using CoinsApplication.Models;
using CoinsApplication.Services;
using CoinsApplication.Services.Interfaces;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IProfileService _profileService;
        private readonly ICountryService _countryService;

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


        public MainWindowViewModel(IProfileService profileService, ICountryService countryService)
        {
            _profileService = profileService;
            _countryService = countryService;

            RefreshProfiles();
        }
        /*
        private void RefreshCountries()
        {
            var allCountries = _countryService.GetAllCountries();

            Countries.Clear();

            foreach (var profile in allCountries)
            {
                Countries.Add(new ProfileViewModel(profile));
            }

            SelectedProfile = Profiles.FirstOrDefault();
        }
        */
        private void RefreshProfiles()
        {
            var allProfiles = _profileService.GetAllProfiles();

            Profiles.Clear();

            foreach (var profile in allProfiles)
            {
                Profiles.Add(new ProfileViewModel(profile));
            }

            SelectedProfile = Profiles.FirstOrDefault();
        }
    }   
}
