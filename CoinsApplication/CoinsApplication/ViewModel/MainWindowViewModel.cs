using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.Repositories;
using CoinsApplication.Extensions;
using CoinsApplication.Models;
using CoinsApplication.Models.Factories;
using CoinsApplication.Services.Interfaces.DirtySerializing;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;

namespace CoinsApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ICoinModelFactory _coinModelFactory;
        private readonly IDirtySerializableCacheService _serializableCacheService;

        public ICollectionView CoinsCollectionView { get; set; }

        #region Properties

        public ObservableCollection<CoinModel> Coins { get; } = new ObservableCollection<CoinModel>();

        public CoinModel SelectedCoin => CoinsCollectionView.CurrentItem as CoinModel;

        private CoinModel _newCoin;
        public CoinModel NewCoin
        {
            get { return _newCoin; }
            set { Set(ref _newCoin, value); }
        }

        private bool _isEditOpened;
        public bool IsEditOpened
        {
            get { return _isEditOpened; }
            set { Set(ref _isEditOpened, value); }
        }

        private bool _isAddOpened;
        public bool IsAddOpened
        {
            get { return _isAddOpened; }
            set { Set(ref _isAddOpened, value); }
        }

        private bool _isGroupingPanelVisible;
        public bool IsGroupingPanelVisible
        {
            get { return _isGroupingPanelVisible; }
            set { Set(ref _isGroupingPanelVisible, value); }
        }
        

        #endregion

        #region AddNewCoinCommand

        public RelayCommand<WindowCommandContext> AddNewCoinCommand { get; }

        private void AddNewCoin(WindowCommandContext context)
        {
            try
            {
                Coins.Add(NewCoin);
                CoinsCollectionView.MoveCurrentTo(NewCoin);

                IsAddOpened = false;
                CoinsCollectionView.Refresh();
            }
            catch (Exception ex)
            {
                context.Window.ThrowUnknownErrorMessageBox(ex);
            }
        }

        #endregion

        #region RemoveCoinCommand

        public RelayCommand<WindowCommandContext> RemoveCoinCommand { get; }

        private bool CanRemoveCoin(WindowCommandContext context)
        {
            return CoinsCollectionView.CurrentItem != null;
        }

        private async void RemoveCoin(WindowCommandContext context)
        {
            try
            {
                if (SelectedCoin != null && await context.Window.ThrowWillBeRemovedMessageBox(SelectedCoin) == MessageDialogResult.Affirmative)
                {
                    var isFirst = Coins.FirstOrDefault() == SelectedCoin;
                    var current = SelectedCoin;
                    if (isFirst)
                    {
                        CoinsCollectionView.MoveCurrentToNext();
                    }
                    else
                    {
                        CoinsCollectionView.MoveCurrentToPrevious();
                    }

                    Coins.Remove(current);
                    _serializableCacheService.Remove(current);

                    CoinsCollectionView.Refresh();
                }
            }
            catch (Exception ex)
            {
                context.Window.ThrowUnknownErrorMessageBox(ex);
            }
        }

        #endregion

        #region ShowEditCoinControlCommand

        public RelayCommand ShowEditCoinControlCommand { get; }

        private bool CanShowEditCoinControl()
        {
            return SelectedCoin != null;
        }

        private void ShowEditCoinControl()
        {
            IsEditOpened = true;
        }

        #endregion

        #region ToggleGroupingPanelCommand

        public RelayCommand ToggleGroupingPanelCommand { get; }

        private void ToggleGroupingPanel()
        {
            IsGroupingPanelVisible = !IsGroupingPanelVisible;
        }

        #endregion

        #region ShowAddCoinControlCommand

        public RelayCommand ShowAddCoinControlCommand { get; }

        private void ShowAddCoinControl()
        {
            NewCoin = _coinModelFactory.Create();

            IsAddOpened = true;
        }

        #endregion

        #region SaveAllCommand

        public RelayCommand<WindowCommandContext> SaveAllCommand { get; }

        private bool CanSaveAll(WindowCommandContext context)
        {
            return !_serializableCacheService.IsEmpty;
        }

        private async void SaveAll(WindowCommandContext context)
        {
            var invalidCoins = Coins.Where(x => !x.Validator.ValidateAll().IsValid)
                .Select(x => x.Title)
                .ToList();

            if (invalidCoins.Count == 0)
            {
                _serializableCacheService.Commit();
            }
            else
            {
                await context.Window.ThrowValidationFailedMessageBox(invalidCoins);
            }
        }

        #endregion



        public MainWindowViewModel(ICoinRepository coinRepository, 
            IUnitOfWorkFactory unitOfWorkFactory,
            ICoinModelFactory coinModelFactory,
            IDirtySerializableCacheService serializableCacheService)
        {
            _coinModelFactory = coinModelFactory;
            _serializableCacheService = serializableCacheService;

            RemoveCoinCommand = new RelayCommand<WindowCommandContext>(RemoveCoin, CanRemoveCoin);
            ShowEditCoinControlCommand = new RelayCommand(ShowEditCoinControl, CanShowEditCoinControl);
            ShowAddCoinControlCommand = new RelayCommand(ShowAddCoinControl);
            SaveAllCommand = new RelayCommand<WindowCommandContext>(SaveAll, CanSaveAll);
            AddNewCoinCommand = new RelayCommand<WindowCommandContext>(AddNewCoin);
            ToggleGroupingPanelCommand = new RelayCommand(ToggleGroupingPanel);

            _serializableCacheService.CacheChanged += CacheChangedHandler;

            using (unitOfWorkFactory.Create())
            {
                Coins.AddRange(coinRepository.GetAll().Select(x => _coinModelFactory.Create(x)));
            }

            CoinsCollectionView = CollectionViewSource.GetDefaultView(Coins);
            CoinsCollectionView.CurrentChanged += CoinsCollectionChangedHandler;
        }


        private void CacheChangedHandler(object sender, EventArgs eventArgs)
        {
            SaveAllCommand.RaiseCanExecuteChanged();
        }

        private void CoinsCollectionChangedHandler(object sender, EventArgs e)
        {
            ShowEditCoinControlCommand.RaiseCanExecuteChanged();
            RemoveCoinCommand.RaiseCanExecuteChanged();

            RaisePropertyChanged(() => SelectedCoin);
        }

        public override void Cleanup()
        {
            _serializableCacheService.CacheChanged -= CacheChangedHandler;
            CoinsCollectionView.CurrentChanged -= CoinsCollectionChangedHandler;

            base.Cleanup();
        }
    }   
}
