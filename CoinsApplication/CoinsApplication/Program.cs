using System;
using System.ComponentModel;
using CoinsApplication.Services;
using CoinsApplication.Services.Implementation.Data;
using CoinsApplication.Services.Implementation.System;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.Views;
using GalaSoft.MvvmLight;
using SimpleInjector;
using Container = SimpleInjector.Container;

namespace CoinsApplication
{
    public class Program
    {
        [STAThread]
        private static void Main()
        {
            var container = Bootstrap();

            // Any additional other configuration, e.g. of your desired MVVM toolkit.

            RunApplication(container);
        }

        internal static Container Bootstrap()
        {
            // Create the container as usual.
            var container = new Container();

            // Register your types, for instance:
            container.Register<IDialogService, DialogService>(Lifestyle.Singleton);
            container.Register<IImageReaderService, ImageReaderService>(Lifestyle.Singleton);

            container.Register<IProfileService, ProfileService>(Lifestyle.Singleton);
            container.Register<ICountryService, CountryService>(Lifestyle.Singleton);
            container.Register<ICurrencyService, CurrencyService>(Lifestyle.Singleton);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                //Register design mode specific services
            }

            // Register your windows and view models:
            container.Verify();

            return container;
        }

        private static void RunApplication(Container container)
        {
            try
            {
                var app = new App();
                var mainWindow = container.GetInstance<MainWindow>();
                app.Run(mainWindow);
            }
            catch (Exception ex)
            {
                //Log the exception and exit
            }
        }
    }
}