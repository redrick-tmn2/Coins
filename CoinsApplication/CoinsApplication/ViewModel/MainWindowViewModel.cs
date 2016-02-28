using System.Collections.ObjectModel;
using CoinsApplication.Services;
using CoinsApplication.ViewModel.SampleData;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CoinViewModel> _coins = new ObservableCollection<CoinViewModel>();
 
        public ObservableCollection<CoinViewModel> Coins
        {
            get { return _coins; }
        }

        public MainWindowViewModel(ISampleService sampleService)
        {
            if (IsInDesignMode)
            {
                _coins = FakeCoinViewModelProvider.GetCoinsViewModels();
            }

            //sampleService.SampleMethod();
        }
    }
}
