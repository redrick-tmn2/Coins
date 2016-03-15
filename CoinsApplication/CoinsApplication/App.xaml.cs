using System.Windows;
using System.Xml.Serialization;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.NHibernate;
using CoinsApplication.DAL.NHibernate.Migrator;
using CoinsApplication.DAL.NHibernate.Repository;
using CoinsApplication.DAL.Repositories;
using CoinsApplication.Models.Factories;
using CoinsApplication.Services.Implementation;
using CoinsApplication.Services.Implementation.System;
using CoinsApplication.Services.Interfaces;
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

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MigrateDatabase();

            Bootstrap();

            ShowWindow();
        }

        internal static void ShowWindow()
        {
            var mainWindow = new MainWindow();
            // Show the window
            mainWindow.Show();
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
            
            _container.Register<IDialogService, DialogService>(Lifestyle.Singleton);
            _container.Register<IImageReaderService, ImageReaderService>(Lifestyle.Singleton);

            _container.Register<IUnitOfWorkFactory, NHibernateUnitOfWorkFactory>(Lifestyle.Singleton);
            
            BootstrapRepositories();
            BootstrapViewModels();


            _container.Register<IDirtySerializableCacheService, DirtySerializableCacheService>(Lifestyle.Singleton);
            _container.Register<ICoinModelFactory, CoinModelFactory>(Lifestyle.Singleton);

            if (ViewModelBase.IsInDesignModeStatic)
            {

            }
            
            _container.Verify();

            ServiceLocator.SetLocatorProvider(() => _containerAdapter);
        }

        private static void BootstrapRepositories()
        {
            _container.Register<ICoinRepository, CoinRepository>(Lifestyle.Singleton);
            _container.Register<ICurrencyRepository, CurrencyRepository>(Lifestyle.Singleton);
            _container.Register<ICountryRepository, CountryRepository>(Lifestyle.Singleton);

            _container.Register<ISaveObjectRepository, SaveObjectRepository>(Lifestyle.Singleton);
        }

        private static void BootstrapViewModels()
        {
            _container.Register<MainWindowViewModel>(Lifestyle.Singleton);
            _container.Register<FilterViewModel>(Lifestyle.Singleton);
        }
    }
}
