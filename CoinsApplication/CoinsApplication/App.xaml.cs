using System.Threading.Tasks;
using System.Windows;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.NHibernate;
using CoinsApplication.DAL.NHibernate.Migrator;
using CoinsApplication.DAL.NHibernate.Repository;
using CoinsApplication.DAL.Repositories;
using CoinsApplication.Models.Factories;
using CoinsApplication.Services.ImageCaching;
using CoinsApplication.Services.Implementation;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.Services.Interfaces.DirtySerializing;
using CoinsApplication.Services.Interfaces.ImageCaching;
using CoinsApplication.Services.Interfaces.Logging;
using CoinsApplication.Services.Logging;
using CoinsApplication.ViewModel;
using CoinsApplication.Views;
using CommonServiceLocator.SimpleInjectorAdapter;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using SimpleInjector;

namespace CoinsApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        private static Container _container;
        private static SimpleInjectorServiceLocatorAdapter _containerAdapter;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var splash = ShowSplashWindow();

            await BootstrapAsync();

            splash.Close();

            ShowWindow();
        }

        private static Task BootstrapAsync()
        {
            return new TaskFactory().StartNew(() =>
            {
                MigrateDatabase();

                Bootstrap();
            });
        }

        internal static Window ShowWindow()
        {
            var mainWindow = _container.GetInstance<MainWindow>();
            // Show the window
            mainWindow.Show();

            return mainWindow;
        }

        internal static Window ShowSplashWindow()
        {
            var splashWindow = new SplashWindow();
            // Show the window
            splashWindow.Show();

            return splashWindow;
        }

        internal static void MigrateDatabase()
        {
            var migrator = new SqLiteMigrator();
#if DEBUG
         //   migrator.Migrate(runner => runner.MigrateDown(0));
#endif
            migrator.Migrate(runner => runner.MigrateUp());


        }

        internal static void Bootstrap()
        {
            _container = new Container();
            _containerAdapter = new SimpleInjectorServiceLocatorAdapter(_container);
            
            _container.Register<ILoggingService, LoggingService>(Lifestyle.Singleton);
            _container.Register<IDialogService, DialogService>(Lifestyle.Singleton);
            _container.Register<IImageReaderService, ImageReaderService>(Lifestyle.Singleton);

            _container.Register<IUnitOfWorkFactory, NHibernateUnitOfWorkFactory>(Lifestyle.Singleton);
            
            BootstrapRepositories();
            BootstrapViewModels();


            _container.Register<IDirtySerializableCacheService, DirtySerializableCacheService>(Lifestyle.Singleton);
            _container.Register<ICoinModelFactory, CoinModelFactory>(Lifestyle.Singleton);
            _container.Register<IImageCacheService, ImageCacheService>(Lifestyle.Singleton);

            if (ViewModelBase.IsInDesignModeStatic)
            {

            }

            ServiceLocator.SetLocatorProvider(() => _containerAdapter);
        }

        private static void BootstrapRepositories()
        {
            _container.Register<ICoinRepository, CoinRepository>(Lifestyle.Singleton);
            _container.Register<ICurrencyRepository, CurrencyRepository>(Lifestyle.Singleton);
            _container.Register<ICountryRepository, CountryRepository>(Lifestyle.Singleton);
            _container.Register<IImageRepository, ImageRepository>(Lifestyle.Singleton);

            _container.Register<ISaveObjectRepository, SaveObjectRepository>(Lifestyle.Singleton);
        }

        private static void BootstrapViewModels()
        {
            _container.Register<CountriesViewModel>(Lifestyle.Singleton);
            _container.Register<CurrenciesViewModel>(Lifestyle.Singleton);
            _container.Register<MainWindowViewModel>(Lifestyle.Singleton);
            _container.Register<FilterViewModel>(Lifestyle.Singleton);
        }
    }
}
