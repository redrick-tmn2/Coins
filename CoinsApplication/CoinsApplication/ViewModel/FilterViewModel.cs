using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoinsApplication.Extensions;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.ViewModel.SelectableViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CoinsApplication.ViewModel
{
    public class FilterViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        private readonly IEnumerable<string> _activeFilterProperties = new List<string>
        {
            nameof(CoinModel.Country),
            nameof(CoinModel.Currency),
            nameof(CoinModel.Title)
        };

        #region ClearTitleFilterPatternCommand

        public RelayCommand ClearTitleFilterPatternCommand { get; }

        private void ClearTitleFilterPattern()
        {
            TitleFilterPattern = string.Empty;
        }

        #endregion

        #region public properties

        private string _titleFilterPattern;
        public string TitleFilterPattern
        {
            get { return _titleFilterPattern; }
            set
            {
                Set(ref _titleFilterPattern, value);
                _mainWindowViewModel.CoinsCollectionView.Refresh();
            }
        }

        public List<SelectableViewModelBase<CountryModel>> Countries { get; } = new List<SelectableViewModelBase<CountryModel>>();

        public List<SelectableViewModelBase<CurrencyModel>> Currencies { get; } = new List<SelectableViewModelBase<CurrencyModel>>();

        #endregion

        #region ctor

        public FilterViewModel(ICountryService countryService,
            ICurrencyService currencyService,
            MainWindowViewModel mainViewModel)
        {
            _mainWindowViewModel = mainViewModel;

            ClearTitleFilterPatternCommand = new RelayCommand(ClearTitleFilterPattern);

            RefreshCurrencies(currencyService);
            RefreshCountries(countryService);

            SetFilter();
        }

        #endregion

        #region private members

        private void SetFilter()
        {
            _mainWindowViewModel.CoinsCollectionView.Filter = item =>
            {
                var coin = item as CoinModel;
                if (coin == null)
                {
                    return false;
                }

                return IsTitlePassed(coin) && IsCurrencyPassed(coin) && IsCountryPassed(coin);
            };

            ActiveLiveFiltering(_mainWindowViewModel.CoinsCollectionView, _activeFilterProperties);
        }
        
        private void ActiveLiveFiltering(ICollectionView collectionView, IEnumerable<string> involvedProperties)
        {
            var collectionViewLiveShaping = collectionView as ICollectionViewLiveShaping;

            if (collectionViewLiveShaping != null && collectionViewLiveShaping.CanChangeLiveFiltering)
            {
                foreach (var propName in involvedProperties)
                    collectionViewLiveShaping.LiveFilteringProperties.Add(propName);
                collectionViewLiveShaping.IsLiveFiltering = true;
            }
        }

        private bool IsTitlePassed(CoinModel coin)
        {
            return _titleFilterPattern == null ||
                   coin.Title.Contains(_titleFilterPattern, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsCurrencyPassed(CoinModel coin)
        {
            return Currencies.Any(x => x.IsSelected && x.Model == coin.Currency);
        }

        private bool IsCountryPassed(CoinModel coin)
        {
            return Countries.Any(x => x.IsSelected && x.Model == coin.Country);
        }

        private void RefreshCurrencies(ICurrencyService currencyService)
        {
            Currencies.Add(SelectableViewModelBase<CurrencyModel>.Empty);
            Currencies.AddRange(currencyService.GetAllCurrencies().Select(x => new SelectableViewModelBase<CurrencyModel>(x)));

            foreach (var currencyViewModel in Currencies)
            {
                currencyViewModel.IsSelectedChanged += IsSelectedChanged;
            }
        }

        private void RefreshCountries(ICountryService countryService)
        {
            Countries.Add(SelectableViewModelBase<CountryModel>.Empty);
            Countries.AddRange(countryService.GetAllCountries().Select(x => new SelectableViewModelBase<CountryModel>(x)));

            foreach (var countryViewModel in Countries)
            {
                countryViewModel.IsSelectedChanged += IsSelectedChanged;
            }
        }

        private void ClearCurrencies()
        {
            foreach (var currency in Currencies)
            {
                currency.IsSelectedChanged -= IsSelectedChanged;
            }

            Currencies.Clear();
        }

        private void ClearCountries()
        {
            foreach (var country in Countries)
            {
                country.IsSelectedChanged -= IsSelectedChanged;
            }

            Countries.Clear();
        }

        private void IsSelectedChanged(object sender, EventArgs e)
        {
            _mainWindowViewModel.CoinsCollectionView.Refresh();
        }

        #endregion

        #region ViewModelBase members

        public override void Cleanup()
        {
            ClearCountries();
            ClearCurrencies();

            base.Cleanup();
        }

        #endregion
    }
}
