using CoinsApplication.FakeServices;

namespace CoinsApplication.ViewModel.SampleData
{
    public class FakeCoinViewModelProvider
    {
        public static MainWindowViewModel MainWindowViewModel
        {
            get { return new MainWindowViewModel(new FakeProfileService()); }
        }
    }
}
