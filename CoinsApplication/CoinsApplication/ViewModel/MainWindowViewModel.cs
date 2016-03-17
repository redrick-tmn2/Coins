using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.Repositories;
using CoinsApplication.Extensions;
using CoinsApplication.Models;
using CoinsApplication.Models.Factories;
using CoinsApplication.Properties;
using CoinsApplication.Services.Implementation;
using CoinsApplication.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;

namespace CoinsApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IImageReaderService _imageReaderService;
        private readonly ICoinModelFactory _coinModelFactory;
        private readonly IDirtySerializableCacheService _serializableCacheService;

        public ICollectionView CoinsCollectionView { get; set; }

        public IWindowWrapper Window { get; set; }

        #region Collections

        public ObservableCollection<CoinModel> Coins { get; } = new ObservableCollection<CoinModel>();

        public List<Country> Countries { get; } = new List<Country>();

        public List<Currency> Currencies { get; } = new List<Currency>();

        #endregion

        #region Properties
        public CoinModel SelectedCoin => CoinsCollectionView.CurrentItem as CoinModel;

        private bool _isEditOpened;
        public bool IsEditOpened
        {
            get { return _isEditOpened; }
            set { Set(ref _isEditOpened, value); }
        }


        #endregion

        #region UpdateCoinImageCommand

        public RelayCommand<CoinModel> UpdateCoinImageCommand { get; }

        private bool CanUpdateCoinImage(CoinModel coinModel)
        {
            return coinModel != null;
        }

        private void UpdateCoinImage(CoinModel coinModel)
        {
            try
            {
                var fileName = _dialogService.ShowOpenFileDialog();
                if (string.IsNullOrEmpty(fileName))
                {
                    return;
                }

                coinModel.Image = _imageReaderService.ReadImage(fileName);
                IsEditOpened = true;
            }
            catch (Exception ex)
            {
                ThrowUnknownErrorMessageBox(ex);
            }
        }

        #endregion

        #region AddNewCoinCommand

        public RelayCommand AddNewCoinCommand { get; }

        private void AddNewCoin()
        {
            try
            {
                var coinModel = _coinModelFactory.Create();

                Coins.Add(coinModel);
                CoinsCollectionView.MoveCurrentTo(coinModel);

                IsEditOpened = true;
                CoinsCollectionView.Refresh();
            }
            catch (Exception ex)
            {
                ThrowUnknownErrorMessageBox(ex);
            }
        }

        #endregion

        #region RemoveCoinCommand

        public RelayCommand<CoinModel> RemoveCoinCommand { get; }

        private bool CanRemoveCoin(CoinModel coin)
        {
            return CoinsCollectionView.CurrentItem != null;
        }

        private async void RemoveCoin(CoinModel coin)
        {
            try
            {
                if (SelectedCoin != null && await ThrowWillBeRemovedMessageBox(SelectedCoin) == MessageDialogResult.Affirmative)
                {
                    
                    Coins.Remove(SelectedCoin);

                    CoinsCollectionView.MoveCurrentToNext();
                    CoinsCollectionView.Refresh();
                }
            }
            catch (Exception ex)
            {
                ThrowUnknownErrorMessageBox(ex);
            }
        }

        #endregion

        #region EditCoinCommand

        public RelayCommand<CoinModel> EditCoinCommand { get; }

        private bool CanEditCoin(CoinModel coin)
        {
            return SelectedCoin != null;
        }

        private void EditCoin(CoinModel coin)
        {
            IsEditOpened = true;
        }

        #endregion

        #region SaveAllCommand

        public RelayCommand SaveAllCommand { get; }

        private bool CanSaveAll()
        {
            return !_serializableCacheService.IsEmpty;
        }

        private void SaveAll()
        {
            _serializableCacheService.SaveAll();
        }

        #endregion

        public MainWindowViewModel(ICoinRepository coinRepository, 
            ICountryRepository countryRepository, 
            ICurrencyRepository currencyRepository, 
            IDialogService dialogService, 
            IImageReaderService imageReaderService,
            IUnitOfWorkFactory unitOfWorkFactory,
            ICoinModelFactory coinModelFactory,
            IDirtySerializableCacheService serializableCacheService)
        {
            _dialogService = dialogService;
            _imageReaderService = imageReaderService;
            _coinModelFactory = coinModelFactory;
            _serializableCacheService = serializableCacheService;

            UpdateCoinImageCommand = new RelayCommand<CoinModel>(UpdateCoinImage, CanUpdateCoinImage);
            
            RemoveCoinCommand = new RelayCommand<CoinModel>(RemoveCoin, CanRemoveCoin);
            EditCoinCommand = new RelayCommand<CoinModel>(EditCoin, CanEditCoin);
            SaveAllCommand = new RelayCommand(SaveAll, CanSaveAll);
            AddNewCoinCommand = new RelayCommand(AddNewCoin);

            _serializableCacheService.CacheChanged += CacheChangedHandler;

            using (unitOfWorkFactory.Create())
            {
                Countries.AddRange(countryRepository.GetAll());
                Currencies.AddRange(currencyRepository.GetAll());
                Coins.AddRange(coinRepository.GetAll().Select(x => _coinModelFactory.Create(x)));
            }

            CoinsCollectionView = CollectionViewSource.GetDefaultView(Coins);
            CoinsCollectionView.CurrentChanged += CoinsCollectionView_CurrentChanged;
        }

        private void CoinsCollectionView_CurrentChanged(object sender, EventArgs e)
        {
            EditCoinCommand.RaiseCanExecuteChanged();
            UpdateCoinImageCommand.RaiseCanExecuteChanged();
            RemoveCoinCommand.RaiseCanExecuteChanged();
        }

        private void CacheChangedHandler(object sender, EventArgs eventArgs)
        {
            SaveAllCommand.RaiseCanExecuteChanged();
        }

        private async void ThrowUnknownErrorMessageBox(Exception ex)
        {
            await Window.ShowMessageAsync(Resources.UnknownErrorMessageBoxTitle,
                string.Format(Resources.UnknownErrorMessageBoxText, ex.Message), DialogStyle.Affirmative);
        }

        private async Task<MessageDialogResult> ThrowWillBeRemovedMessageBox(CoinModel coin)
        {
            var message = string.Format(Resources.RemoveCoinMessageBoxText, coin.Title);
            return await Window.ShowMessageAsync(Resources.RemoveCoinMessageBoxTtitle, message, DialogStyle.AffirmativeAndNegative);
        }
    }   
}
