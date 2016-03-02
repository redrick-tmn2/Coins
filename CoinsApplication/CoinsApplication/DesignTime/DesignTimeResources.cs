using CoinsApplication.Services;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.ViewModel;
using SimpleInjector;

namespace CoinsApplication.DesignTime
{
    public class DesignTimeResources
    {
        private static Container _container;

        private static Container Bootstrap()
        {
            // Create the container as usual.
            var container = new Container();

            // Register your types, for instance:
            container.Register<ICountryService, CountryService>(Lifestyle.Singleton);
            container.Register<ICurrencyService, CurrencyService>(Lifestyle.Singleton);
            container.Register<IProfileService, ProfileService>(Lifestyle.Singleton);

            // Register your windows and view models:
            container.Verify();

            return container;
        }

        public static Container Container
        {
            get { return _container ?? (_container = Bootstrap()); }
        }

        public static MainWindowViewModel MainWindowViewModel
        {
            get { return Container.GetInstance<MainWindowViewModel>(); }
        }
    }
}
