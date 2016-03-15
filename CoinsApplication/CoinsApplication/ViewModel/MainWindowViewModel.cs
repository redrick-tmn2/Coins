using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.Repositories;
using CoinsApplication.Extensions;
using CoinsApplication.Models;
using CoinsApplication.Models.Factories;
using CoinsApplication.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CoinsApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IImageReaderService _imageReaderService;
        private readonly ICoinModelFactory _coinModelFactory;
        private readonly IDirtySerializableCacheService _serializableCacheService;

        public ICollectionView CoinsCollectionView { get; set; }

        #region Collections

        public ObservableCollection<CoinModel> Coins { get; } = new ObservableCollection<CoinModel>();

        public List<Country> Countries { get; } = new List<Country>();

        public List<Currency> Currencies { get; } = new List<Currency>();

        #endregion

        #region Properties

        private CoinModel _selectedCoin;

        public CoinModel SelectedCoin
        {
            get { return _selectedCoin; }
            set
            {
                Set(ref _selectedCoin, value);

                EditCoinCommand.RaiseCanExecuteChanged();
                UpdateCoinImageCommand.RaiseCanExecuteChanged();
                RemoveCoinCommand.RaiseCanExecuteChanged();
            }
        }

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
                _dialogService.ShowMessageBox(ex.Message);
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
                SelectedCoin = coinModel;

                IsEditOpened = true;
                CoinsCollectionView.Refresh();
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessageBox(ex.Message);
            }
        }

        #endregion

        #region RemoveCoinCommand

        public RelayCommand<CoinModel> RemoveCoinCommand { get; }

        private bool CanRemoveCoin(CoinModel coin)
        {
            return coin != null;
        }

        private void RemoveCoin(CoinModel coin)
        {
            try
            {
                if (coin != null)
                {
                    Coins.Remove(coin);
                    CoinsCollectionView.Refresh();
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessageBox(ex.Message);
            }
        }

        #endregion

        #region EditCoinCommand

        public RelayCommand<CoinModel> EditCoinCommand { get; }

        private bool CanEditCoin(CoinModel coin)
        {
            return coin != null;
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
        }

        private void CacheChangedHandler(object sender, EventArgs eventArgs)
        {
            SaveAllCommand.RaiseCanExecuteChanged();
        }
    }   
}
