using CoinsApplication.FakeServices;
using CoinsApplication.Services;

namespace CoinsApplication.ViewModel.SampleData
{
    public class FakeCoinViewModelProvider
    {
        public static MainWindowViewModel MainWindowViewModel
        {
            get { return new MainWindowViewModel(new ProfileService()); }
        }
    }
}
