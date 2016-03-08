namespace CoinsApplication.ViewModel
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Container.GetInstance<MainWindowViewModel>();

        public FilterViewModel FilterViewModel => App.Container.GetInstance<FilterViewModel>();
    }
}
