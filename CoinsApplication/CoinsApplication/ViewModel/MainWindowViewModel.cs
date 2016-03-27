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
using CoinsApplication.Services.Interfaces;
using CoinsApplication.Services.Interfaces.DirtySerializing;
using CoinsApplication.Services.Interfaces.ImageCaching;
using CoinsApplication.Services.Interfaces.Logging;
using CoinsApplication.Services.Interfaces.Window;
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
        private readonly IImageCacheService _imageCacheService;
        private readonly ILoggingService _loggingService;

        private  IWindowWrapper _window;

        public ICollectionView CoinsCollectionView { get; set; }

        public IWindowWrapper Window
        {
            get { return _window; }
            set
            {
                if (_window != null)
                {
                    _window.Closing -= WindowClosingHandler;
                }

                _window = value;
                _window.Closing += WindowClosingHandler;
            }
        }

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

        #region AddCoinImageCommand

        public RelayCommand RemoveCoinImageCommand { get; }

        private void RemoveCoinImage()
        {
            try
            {
                if (SelectedCoin?.SelectedImage == null)
                {
                    return;
                }

                var itemToRemove = SelectedCoin.SelectedImage;
                SelectedCoin.SelectedImage = SelectedCoin.Images.PreviousOrNext(itemToRemove);
                _imageCacheService.Remove(itemToRemove.Content);
                SelectedCoin.Images.Remove(itemToRemove);
                SelectedCoin.SetDirty();
            }
            catch (Exception ex)
            {
                ThrowUnknownErrorMessageBox(ex);
            }
        }

        #endregion

        #region AddCoinImageCommand

        public RelayCommand AddCoinImageCommand { get; }

        private bool CanAddCoinImage()
        {
            return CoinsCollectionView.CurrentItem != null;
        }

        private void AddCoinImage()
        {
            try
            {
                if (SelectedCoin != null)
                {
                    var fileName = _dialogService.ShowOpenFileDialog();
                    if (string.IsNullOrEmpty(fileName))
                    {
                        return;
                    }

                    var imageBytes = _imageReaderService.ReadImage(fileName);
                    var image = new Image
                    {
                        Content = imageBytes,
                    };

                    SelectedCoin.AddImage(image);
                    SelectedCoin.SelectedImage = image;
                    SelectedCoin.SetDirty();
                }
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

        public RelayCommand RemoveCoinCommand { get; }

        private bool CanRemoveCoin()
        {
            return CoinsCollectionView.CurrentItem != null;
        }

        private async void RemoveCoin()
        {
            try
            {
                if (SelectedCoin != null && await ThrowWillBeRemovedMessageBox(SelectedCoin) == DialogResult.Affirmative)
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
                ThrowUnknownErrorMessageBox(ex);
            }
        }

        #endregion

        #region EditCoinCommand

        public RelayCommand EditCoinCommand { get; }

        private bool CanEditCoin()
        {
            return SelectedCoin != null;
        }

        private void EditCoin()
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

        private async void SaveAll()
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
                await ThrowValidationFailedMessageBox(invalidCoins);
            }
        }

        #endregion

        public MainWindowViewModel(ICoinRepository coinRepository, 
            ICountryRepository countryRepository, 
            ICurrencyRepository currencyRepository, 
            IDialogService dialogService, 
            IImageReaderService imageReaderService,
            IUnitOfWorkFactory unitOfWorkFactory,
            ICoinModelFactory coinModelFactory,
            IDirtySerializableCacheService serializableCacheService,
            IImageCacheService imageCacheService,
            ILoggingService loggingService)
        {
            _dialogService = dialogService;
            _imageReaderService = imageReaderService;
            _coinModelFactory = coinModelFactory;
            _serializableCacheService = serializableCacheService;
            _imageCacheService = imageCacheService;
            _loggingService = loggingService;

            AddCoinImageCommand = new RelayCommand(AddCoinImage, CanAddCoinImage);
            RemoveCoinImageCommand = new RelayCommand(RemoveCoinImage);
            RemoveCoinCommand = new RelayCommand(RemoveCoin, CanRemoveCoin);
            EditCoinCommand = new RelayCommand(EditCoin, CanEditCoin);
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
            CoinsCollectionView.CurrentChanged += CoinsCollectionChangedHandler;
        }

        private async void ThrowUnknownErrorMessageBox(Exception ex)
        {
            _loggingService.Error(Resources.UnknownErrorMessageBoxTitle, ex);
            await Window.ShowMessageAsync(Resources.UnknownErrorMessageBoxTitle
                , string.Format(Resources.UnknownErrorMessageBoxText, ex.Message)
                , DialogStyle.Affirmative);
        }

        private async Task<DialogResult> ThrowWillBeRemovedMessageBox(CoinModel coin)
        {
            var message = string.Format(Resources.RemoveCoinMessageBoxText, coin.Title);
            return await Window.ShowMessageAsync(Resources.RemoveCoinMessageBoxTtitle
                , message
                , DialogStyle.AffirmativeAndNegative
                , new DialogSettings
                {
                    AffirmativeButtonText = "Yes",
                    NegativeButtonText = "No"
                });
        }

        private async Task<DialogResult> ThrowValidationFailedMessageBox(IEnumerable<string> invalidCoins)
        {
            var invalidCoinsMessage = Environment.NewLine + string.Join(Environment.NewLine, invalidCoins);

            var message = string.Format(Resources.ValidationFailedMessageBoxText, invalidCoinsMessage);
            return await Window.ShowMessageAsync(Resources.ValidationFailedMessageBoxTitle
                , message
                , DialogStyle.Affirmative);
        }

        //private async Task<DialogResult> ThrowClosingMessageBox()
        //{
        //    return await Window.ShowMessageAsync(Resources.ClosingMessageBoxTitle
        //        , Resources.ClosingMessageBoxText
        //        , DialogStyle.AffirmativeAndNegative
        //        , new DialogSettings
        //        {
        //            AffirmativeButtonText = "Yes",
        //            NegativeButtonText = "No"
        //        });
        //}

        private void CacheChangedHandler(object sender, EventArgs eventArgs)
        {
            SaveAllCommand.RaiseCanExecuteChanged();
        }

        private void WindowClosingHandler(object sender, CancelEventArgs e)
        {
            //if (!_serializableCacheService.IsEmpty)
            //{
            //    var result = ThrowClosingMessageBox();
            //    result.Wait();

            //    e.Cancel = result.Result == DialogResult.Affirmative;
            //}
        }

        private void CoinsCollectionChangedHandler(object sender, EventArgs e)
        {
            EditCoinCommand.RaiseCanExecuteChanged();
            AddCoinImageCommand.RaiseCanExecuteChanged();
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
