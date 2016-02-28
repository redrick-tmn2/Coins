using System.Collections.ObjectModel;

namespace CoinsApplication.ViewModel.SampleData
{
    public class FakeCoinViewModelProvider
    {
        public static ObservableCollection<CoinViewModel> GetCoinsViewModels()
        {
            return new ObservableCollection<CoinViewModel>
            {
                new CoinViewModel { Title = "Один рубль" },
                new CoinViewModel { Title = "Два рубля" },
                new CoinViewModel { Title = "Пять рублей" }
            };
        }

        public static MainWindowViewModel MainWindowViewModel
        {
            get { return new MainWindowViewModel(null); }
        }
    }
}
