using Microsoft.Practices.ServiceLocation;

namespace CoinsApplication.ViewModel
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();

        public CurrenciesViewModel CurrenciesViewModel => ServiceLocator.Current.GetInstance<CurrenciesViewModel>();

        public CountriesViewModel CountriesViewModel => ServiceLocator.Current.GetInstance<CountriesViewModel>();

        public FilterViewModel FilterViewModel => ServiceLocator.Current.GetInstance<FilterViewModel>();
    }
}
