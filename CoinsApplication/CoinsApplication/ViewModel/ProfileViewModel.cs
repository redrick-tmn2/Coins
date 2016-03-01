using System.Linq;
using CoinsApplication.Models;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly ProfileModel _model;
        public ProfileModel Model
        {
            get { return _model; }
        }

        private CoinModel _selectedCoin;
        public CoinModel SelectedCoin
        {
            get { return _selectedCoin; }
            set { Set(ref _selectedCoin, value); }
        }

        public ProfileViewModel(ProfileModel model)
        {
            _model = model;

            SelectedCoin = model.Coins.FirstOrDefault();
        }
    }
}
