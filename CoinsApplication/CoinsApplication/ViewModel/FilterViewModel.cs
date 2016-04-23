using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.Repositories;
using CoinsApplication.Extensions;
using CoinsApplication.Models;
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

        public Dictionary<string, SortContext> SortContexts { get; } = new Dictionary<string, SortContext>();

        public SortContext CountriesSortContext => SortContexts[nameof(CoinModel.Country)];

        public SortContext CurrenciesSortContext => SortContexts[nameof(CoinModel.Currency)];

        public SortContext TitleSortContext => SortContexts[nameof(CoinModel.Title)];

        #endregion

        #region CheckAllCommand\UncheckAllCommand

        public RelayCommand<IEnumerable<ISelectable>> CheckAllCommand { get; }
        public RelayCommand<IEnumerable<ISelectable>> UncheckAllCommand { get; }

        private void ToggleAll(IEnumerable<ISelectable> selectables, bool value)
        {
            foreach (var selectable in selectables)
            {
                selectable.IsSelected = value;
            }
        }

        #endregion

        #region SetGroupCommand

        public RelayCommand<string> SetGroupCommand { get; }

        private void SetGroup(string propertyName)
        {
            ToggleGrouping(_mainWindowViewModel.CoinsCollectionView, propertyName);
        }

        #endregion

        #region SetSortCommand

        public RelayCommand<SortContext> SetSortCommand { get; }

        private void SetSort(SortContext context)
        {
            if (context == null)
                return;

            context.ToggleOrder();
            ToggleSorting(_mainWindowViewModel.CoinsCollectionView);
        }

        #endregion

        #region ctor

        public FilterViewModel(ICountryRepository countryRepository,
            ICurrencyRepository currencyRepository,
            IUnitOfWorkFactory unitOfWorkFactory,
            MainWindowViewModel mainViewModel)
        {
            _mainWindowViewModel = mainViewModel;

            foreach (var activeFilterProperty in this._activeFilterProperties)
            {
                SortContexts.Add(activeFilterProperty, new SortContext());
            }

            CheckAllCommand = new RelayCommand<IEnumerable<ISelectable>>(x => ToggleAll(x, true));
            UncheckAllCommand = new RelayCommand<IEnumerable<ISelectable>>(x => ToggleAll(x, false));
            SetGroupCommand = new RelayCommand<string>(SetGroup);
            SetSortCommand = new RelayCommand<SortContext>(SetSort);

            using (unitOfWorkFactory.Create())
            {
                RefreshCurrencies(currencyRepository);
                RefreshCountries(countryRepository);
            }

            Apply();
        }

        #endregion

        #region private members

        private void Apply()
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

            ActivateLiveFiltering(_mainWindowViewModel.CoinsCollectionView, _activeFilterProperties);
            ActivateLiveSorting(_mainWindowViewModel.CoinsCollectionView, _activeFilterProperties);
            ActivateLiveGrouping(_mainWindowViewModel.CoinsCollectionView, _activeFilterProperties);
        }

        private void ToggleGrouping(ICollectionView collectionView, string groupPropertyName)
        {
            if (_activeFilterProperties.Contains(groupPropertyName))
            {
                var groupDescription = collectionView.GroupDescriptions.OfType<PropertyGroupDescription>()
                    .FirstOrDefault(x => x.PropertyName == groupPropertyName);

                if (groupDescription == null)
                {
                    collectionView.GroupDescriptions.Add(new PropertyGroupDescription(groupPropertyName));
                }
                else
                {
                    collectionView.GroupDescriptions.Remove(groupDescription);
                }
            }
        }

        private void ToggleSorting(ICollectionView collectionView)
        {
            collectionView.SortDescriptions.Clear();
            foreach (var sortContext in SortContexts)
            {
                if (sortContext.Value.Order == Order.Asc)
                {
                    collectionView.SortDescriptions.Add(new SortDescription(sortContext.Key, ListSortDirection.Ascending));
                }

                if (sortContext.Value.Order == Order.Desc)
                {
                    collectionView.SortDescriptions.Add(new SortDescription(sortContext.Key, ListSortDirection.Descending));
                }
            }
        }

        private void ActivateLiveFiltering(ICollectionView collectionView, IEnumerable<string> involvedProperties)
        {
            var collectionViewLiveShaping = collectionView as ICollectionViewLiveShaping;

            if (collectionViewLiveShaping != null && collectionViewLiveShaping.CanChangeLiveFiltering)
            {
                foreach (var propName in involvedProperties)
                    collectionViewLiveShaping.LiveFilteringProperties.Add(propName);
                collectionViewLiveShaping.IsLiveFiltering = true;
            }
        }

        private void ActivateLiveSorting(ICollectionView collectionView, IEnumerable<string> involvedProperties)
        {
            var collectionViewLiveShaping = collectionView as ICollectionViewLiveShaping;

            if (collectionViewLiveShaping != null && collectionViewLiveShaping.CanChangeLiveSorting)
            {
                foreach (var propName in involvedProperties)
                    collectionViewLiveShaping.LiveSortingProperties.Add(propName);
                collectionViewLiveShaping.IsLiveSorting = true;
            }
        }

        private void ActivateLiveGrouping(ICollectionView collectionView, IEnumerable<string> involvedProperties)
        {
            var collectionViewLiveShaping = collectionView as ICollectionViewLiveShaping;

            if (collectionViewLiveShaping != null && collectionViewLiveShaping.CanChangeLiveGrouping)
            {
                foreach (var propName in involvedProperties)
                    collectionViewLiveShaping.LiveGroupingProperties.Add(propName);
                collectionViewLiveShaping.IsLiveGrouping = true;
            }
        }

        private bool IsTitlePassed(CoinModel coin)
        {
            return string.IsNullOrEmpty(_titleFilterPattern) ||
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
            Currencies.AddRange(currencyService.GetAll().Select(x => new SelectableViewModelBase<Currency>(x)));

            foreach (var currencyViewModel in Currencies)
            {
                currencyViewModel.IsSelectedChanged += IsSelectedChanged;
            }
        }

        private void RefreshCountries(ICountryRepository countryService)
        {
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
