using System.Collections.ObjectModel;
using System.Linq;
using CoinsApplication.Services;
using CoinsApplication.Services.Interfaces;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IProfileService _profileService;

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
        

        public MainWindowViewModel(IProfileService profileService)
        {
            _profileService = profileService;

            RefreshProfiles();
        }

        public void RefreshProfiles()
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
