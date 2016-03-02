using System;
using CoinsApplication.Services;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.Views;
using SimpleInjector;

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

        private static Container Bootstrap()
        {
            // Create the container as usual.
            var container = new Container();

            // Register your types, for instance:
            container.Register<IProfileService, ProfileService>(Lifestyle.Singleton);
            container.Register<ICountryService, CountryService>(Lifestyle.Singleton);
            container.Register<ICurrencyService, CurrencyService>(Lifestyle.Singleton);

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