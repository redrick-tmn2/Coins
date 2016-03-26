using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.Repositories;
using CoinsApplication.Extensions;
using CoinsApplication.Models;
using CoinsApplication.ViewModel.SelectableViewModel;
using GalaSoft.MvvmLight;

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

        public List<SelectableViewModelBase<Country>> Countries { get; } = new List<SelectableViewModelBase<Country>>();

        public List<SelectableViewModelBase<Currency>> Currencies { get; } = new List<SelectableViewModelBase<Currency>>();

        #endregion

        #region ctor

        public FilterViewModel(ICountryRepository countryRepository,
            ICurrencyRepository currencyRepository,
            IUnitOfWorkFactory unitOfWorkFactory,
            MainWindowViewModel mainViewModel)
        {
            _mainWindowViewModel = mainViewModel;
            
            using (unitOfWorkFactory.Create())
            {
                RefreshCurrencies(currencyRepository);
                RefreshCountries(countryRepository);
            }

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
            return Currencies.Any(x => x.IsPassed(coin.Currency));
        }

        private bool IsCountryPassed(CoinModel coin)
        {
            return Countries.Any(x => x.IsPassed(coin.Country));
        }

        private void RefreshCurrencies(ICurrencyRepository currencyService)
        {
            Currencies.Add(SelectableViewModelBase<Currency>.Empty);
            Currencies.AddRange(currencyService.GetAll().Select(x => new SelectableViewModelBase<Currency>(x)));

            foreach (var currencyViewModel in Currencies)
            {
                currencyViewModel.IsSelectedChanged += IsSelectedChanged;
            }
        }

        private void RefreshCountries(ICountryRepository countryService)
        {
            Countries.Add(SelectableViewModelBase<Country>.Empty);
            Countries.AddRange(countryService.GetAll().Select(x => new SelectableViewModelBase<Country>(x)));

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
