using System;
using System.Linq;
using System.Windows.Input;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CoinsApplication.ViewModel
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IImageReaderService _imageReaderService;

        public ProfileModel Model { get; }

        private CoinModel _selectedCoin;
        public CoinModel SelectedCoin
        {
            get { return _selectedCoin; }
            set { Set(ref _selectedCoin, value); }
        }

        #region UpdateCoinImageCommand

        public ICommand UpdateCoinImageCommand { get; }

        private void UpdateCoinImage(CoinModel coinModel)
        {
            try
            {
                var fileName = _dialogService.OpenFileDialog();
                if (string.IsNullOrEmpty(fileName))
                {
                    return;
                }

                var imageBytes = _imageReaderService.ReadImage(fileName);

                if (imageBytes == null)
                {
                    return;
                }

                coinModel.Image = imageBytes;
            }
            catch (Exception ex)
            {
                //TODO: throw error message to user
                throw;
            }
        }

        #endregion

        public ProfileViewModel(ProfileModel model, IDialogService dialogService, IImageReaderService imageReaderService)
        {
            _dialogService = dialogService;
            _imageReaderService = imageReaderService;

            UpdateCoinImageCommand = new RelayCommand<CoinModel>(UpdateCoinImage);

            Model = model;
            SelectedCoin = model.Coins.FirstOrDefault();
        }
    }
}
