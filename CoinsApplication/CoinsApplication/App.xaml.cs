using System;
using System.Windows;
using CoinsApplication.Services;
using SimpleInjector;

namespace CoinsApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Container Bootstrap()
        {
            try
            {            // Create the container as usual.
                var container = new Container();

                // Register your types, for instance:
                container.Register<ISampleService, SampleService>(Lifestyle.Singleton);

                // Register your windows and view models:
                //container.Register<MainWindowViewModel>();

                container.Verify();

                return container;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Bootstrap();

            base.OnStartup(e);

        }
    }
}
