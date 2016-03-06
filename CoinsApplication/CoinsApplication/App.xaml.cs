using CoinsApplication.Services.Implementation.Data;
using CoinsApplication.Services.Implementation.System;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.ViewModel;
using GalaSoft.MvvmLight;
using SimpleInjector;

namespace CoinsApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public class App
    {
        private static Container _container;

        public static Container Container => _container ?? (_container = Bootstrap());

        internal static Container Bootstrap()
        {
            // Create the container as usual.
            var container = new Container();

            // Register your types, for instance:
            container.Register<IDialogService, DialogService>(Lifestyle.Singleton);
            container.Register<IImageReaderService, ImageReaderService>(Lifestyle.Singleton);

            container.Register<ICoinService, CoinService>(Lifestyle.Singleton);
            container.Register<ICountryService, CountryService>(Lifestyle.Singleton);
            container.Register<ICurrencyService, CurrencyService>(Lifestyle.Singleton);

            container.Register<MainWindowViewModel>(Lifestyle.Singleton);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                //Register design mode specific services
            }

            // Register your windows and view models:
            container.Verify();

            return container;
        }
    }

}
