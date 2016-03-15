using System.Windows.Markup;
using Microsoft.Practices.ServiceLocation;

namespace CoinsApplication.ViewModel
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();

        public FilterViewModel FilterViewModel => ServiceLocator.Current.GetInstance<FilterViewModel>();
    }
}
