using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CoinsApplication.Factories;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CoinsApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CoinModelFactory _coinFactory = new CoinModelFactory();

        private readonly IDialogService _dialogService;
        private readonly IImageReaderService _imageReaderService;

        public ICollectionView CoinsCollectionView { get; set; }

        private bool _isEditOpened;
        public bool IsEditOpened
        {
            get { return _isEditOpened; }
            set { Set(ref _isEditOpened, value); }
        }

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

        public ObservableCollection<CoinModel> Coins { get; } = new ObservableCollection<CoinModel>();

        public ObservableCollection<CountryModel> Countries { get; } = new ObservableCollection<CountryModel>();

        public ObservableCollection<CurrencyModel> Currencies { get; } = new ObservableCollection<CurrencyModel>();

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
                var coinModel = _coinFactory.Create();
                Coins.Add(coinModel);
                SelectedCoin = coinModel;
                IsEditOpened = true;
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

        public MainWindowViewModel(ICoinService coinService, 
            ICountryService countryService, 
            ICurrencyService currencyService, 
            IDialogService dialogService, 
            IImageReaderService imageReaderService)
        {
            _dialogService = dialogService;
            _imageReaderService = imageReaderService;

            UpdateCoinImageCommand = new RelayCommand<CoinModel>(UpdateCoinImage, CanUpdateCoinImage);
            AddNewCoinCommand = new RelayCommand(AddNewCoin);
            RemoveCoinCommand = new RelayCommand<CoinModel>(RemoveCoin, CanRemoveCoin);
            EditCoinCommand = new RelayCommand<CoinModel>(EditCoin, CanEditCoin);

            RefreshCountries(countryService);
            RefreshCurrencies(currencyService);
            RefreshCoins(coinService);
        }

        private void RefreshCoins(ICoinService coinService)
        {
            foreach (var coin in coinService.GetAllCoins())
            {
                Coins.Add(coin);
            }

            CoinsCollectionView = CollectionViewSource.GetDefaultView(Coins);
            //CoinsCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Country"));
            //ActiveLiveGrouping(CoinsCollectionView, new[] { "Country", });
        }

        //should be refactored
        //private void ActiveLiveGrouping(ICollectionView collectionView, IList<string> involvedProperties)
        //{
        //    var collectionViewLiveShaping = collectionView as ICollectionViewLiveShaping;
        //    if (collectionViewLiveShaping == null)
        //    {
        //        return;
        //    }

        //    if (collectionViewLiveShaping.CanChangeLiveGrouping)
        //    {
        //        foreach (string propName in involvedProperties)
        //            collectionViewLiveShaping.LiveGroupingProperties.Add(propName);
        //        collectionViewLiveShaping.IsLiveGrouping = true;
        //    }
        //}

        private void RefreshCurrencies(ICurrencyService currencyService)
        {
            Currencies.Clear();

            foreach (var currency in currencyService.GetAllCurrencies())
            {
                Currencies.Add(currency);
            }
        }

        private void RefreshCountries(ICountryService countryService)
        {
            Countries.Clear();

            foreach (var country in countryService.GetAllCountries())
            {
                Countries.Add(country);
            }
        }
    }   
}
