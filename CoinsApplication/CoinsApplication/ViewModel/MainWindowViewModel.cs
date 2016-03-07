using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using CoinsApplication.Extensions;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CoinsApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IImageReaderService _imageReaderService;
        private readonly ICoinService _coinService;

        public ICollectionView CoinsCollectionView { get; set; }

        #region Collections

        public ObservableCollection<CoinModel> Coins { get; } = new ObservableCollection<CoinModel>();

        public ObservableCollection<CountryViewModel> Countries { get; } = new ObservableCollection<CountryViewModel>();

        public ObservableCollection<CurrencyViewModel> Currencies { get; } = new ObservableCollection<CurrencyViewModel>();

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

        private string _titleFilterPattern;

        public string TitleFilterPattern
        {
            get { return _titleFilterPattern; }
            set
            {
                Set(ref _titleFilterPattern, value);
                CoinsCollectionView.Refresh();
            }
        }

        #endregion

        #region ClearTitleFilterPatternCommand

        public RelayCommand ClearTitleFilterPatternCommand { get; }

        private void ClearTitleFilterPattern()
        {
            TitleFilterPattern = string.Empty;
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
                var coinModel = _coinService.CreateNewCoin();
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

        public MainWindowViewModel(ICoinService coinService, 
            ICountryService countryService, 
            ICurrencyService currencyService, 
            IDialogService dialogService, 
            IImageReaderService imageReaderService)
        {
            _dialogService = dialogService;
            _imageReaderService = imageReaderService;
            _coinService = coinService;

            UpdateCoinImageCommand = new RelayCommand<CoinModel>(UpdateCoinImage, CanUpdateCoinImage);
            
            RemoveCoinCommand = new RelayCommand<CoinModel>(RemoveCoin, CanRemoveCoin);
            EditCoinCommand = new RelayCommand<CoinModel>(EditCoin, CanEditCoin);
            AddNewCoinCommand = new RelayCommand(AddNewCoin);

            ClearTitleFilterPatternCommand = new RelayCommand(ClearTitleFilterPattern);

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

            CoinsCollectionView.Filter = item =>
            {
                var coin = item as CoinModel;
                if (coin == null)
                {
                    return false;
                }

                return IsTitlePassed(coin) && IsCurrencyPassed(coin) && IsCountryPassed(coin);
            };

            ActiveLiveFiltering(CoinsCollectionView, new List<string> { "Title", "Country", "Currency" });
        }
        
        //TODO: should be refactored
        private void ActiveLiveFiltering(ICollectionView collectionView, IList<string> involvedProperties)
        {
            var collectionViewLiveShaping = collectionView as ICollectionViewLiveShaping;
            if (collectionViewLiveShaping == null) return;
            if (collectionViewLiveShaping.CanChangeLiveFiltering)
            {
                foreach (string propName in involvedProperties)
                    collectionViewLiveShaping.LiveFilteringProperties.Add(propName);
                collectionViewLiveShaping.IsLiveFiltering = true;
            }
        }

        private bool IsTitlePassed(CoinModel coin)
        {
            return _titleFilterPattern == null || coin.Title.Contains(_titleFilterPattern, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsCurrencyPassed(CoinModel coin)
        {
            return Currencies.Any(x => x.IsSelected && x.Model == coin.Currency);
        }

        private bool IsCountryPassed(CoinModel coin)
        {
            return Countries.Any(x => x.IsSelected && x.Model == coin.Country);
        }

        private void ClearCurrencies()
        {
            foreach (var currency in Currencies)
            {
                currency.IsSelectedChanged -= IsSelectedChanged;
            }

            Currencies.Clear();
        }

        private void RefreshCurrencies(ICurrencyService currencyService)
        {
            ClearCurrencies();
            foreach (var currency in currencyService.GetAllCurrencies())
            {
                var currencyViewModel = new CurrencyViewModel(currency);
                currencyViewModel.IsSelectedChanged += IsSelectedChanged;
                Currencies.Add(currencyViewModel);
            }
        }

        private void ClearCountries()
        {
            foreach (var country in Countries)
            {
                country.IsSelectedChanged -= IsSelectedChanged;
            }

            Countries.Clear();
        }

        private void RefreshCountries(ICountryService countryService)
        {
            ClearCountries();
            foreach (var country in countryService.GetAllCountries())
            {
                var countryViewModel = new CountryViewModel(country);
                countryViewModel.IsSelectedChanged += IsSelectedChanged;
                Countries.Add(countryViewModel);
            }
        }

        private void IsSelectedChanged(object sender, EventArgs eventArgs)
        {
            CoinsCollectionView.Refresh();
        }
    }   
}
