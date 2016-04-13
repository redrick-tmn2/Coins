using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.Extensions;
using CoinsApplication.Properties;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.Services.Interfaces.DirtySerializing;
using CoinsApplication.Services.Interfaces.ImageCaching;
using GalaSoft.MvvmLight.Command;
using MvvmValidation;

namespace CoinsApplication.Models
{
    public class CoinModel : ValidatatbleObservableObject
    {
        const string OpenFileDialogFilter =
       "Image files(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

        private readonly IDialogService _dialogService;
        private readonly IImageReaderService _imageReaderService;
        private readonly IImageCacheService _imageCacheService;

        private readonly Coin _coin;

        public int Id => _coin.Id;

        public ObservableCollection<Image> Images { get; }
        
        public string Title
        {
            get { return _coin.Title; }
            set
            {
                SetAndDirty(_coin.Title, value, () => _coin.Title = value);
                Validator.Validate(() => Title);
            }
        }

        private Image _selectedImage;
        public Image SelectedImage
        {
            get { return _selectedImage; }
            set { Set(ref _selectedImage, value); }
        }
        
        public int? Year
        {
            get { return _coin.Year; }
            set { SetAndDirty(_coin.Year, value, () => _coin.Year = value); }
        }

        public double? Diameter
        {
            get { return _coin.Diameter; }
            set { SetAndDirty(_coin.Diameter, value, () => _coin.Diameter = value); }
        }

        public double? Thickness
        {
            get { return _coin.Thickness; }
            set { SetAndDirty(_coin.Thickness, value, () => _coin.Thickness = value); }
        }

        public Country Country
        {
            get { return _coin.Country; }
            set
            {
                SetAndDirty(_coin.Country, value, () => _coin.Country = value);
                Validator.Validate(() => Country);
            }
        }
        
        public Currency Currency
        {
            get { return _coin.Currency; }
            set
            {
                SetAndDirty(_coin.Currency, value, () => _coin.Currency = value);
                Validator.Validate(() => Currency);
            }
        }


        #region RemoveCoinImageCommand

        public RelayCommand<WindowCommandContext> RemoveCoinImageCommand { get; }

        private void RemoveCoinImage(WindowCommandContext context)
        {
            try
            {
                if (SelectedImage == null)
                {
                    return;
                }

                var itemToRemove = SelectedImage;
                SelectedImage = Images.PreviousOrNext(itemToRemove);
                Images.Remove(itemToRemove);
                _imageCacheService.Remove(itemToRemove.Content);

                SetDirty();
            }
            catch (Exception ex)
            {
                context.Window.ThrowUnknownErrorMessageBox(ex);
            }
        }

        #endregion

        #region AddCoinImageCommand

        public RelayCommand<WindowCommandContext> AddCoinImageCommand { get; }

        private void AddCoinImage(WindowCommandContext context)
        {
            try
            {
                var fileName = _dialogService.ShowOpenFileDialog(OpenFileDialogFilter);
                if (string.IsNullOrEmpty(fileName))
                {
                    return;
                }

                var imageBytes = _imageReaderService.ReadImage(fileName);
                imageBytes.GetOrCreateCachedImage(_imageCacheService);

                var image = new Image
                {
                    Content = imageBytes,
                };

                AddImage(image);
                SelectedImage = image;
                SetDirty();
            }
            catch (NotSupportedException ex)
            {
                context.Window.ThrowUnableToLoadImageErrorMessageBox(ex);
            }
            catch (Exception ex)
            {
                context.Window.ThrowUnknownErrorMessageBox(ex);
            }
        }

        #endregion

        public CoinModel(Coin coin, 
            IDirtySerializableCacheService serializableCacheService,
            IDialogService dialogService,
            IImageReaderService imageReaderService,
            IImageCacheService imageCacheService)
            : base(serializableCacheService)
        {
            _dialogService = dialogService;
            _imageReaderService = imageReaderService;
            _imageCacheService = imageCacheService;

            _coin = coin;

            Images = new ObservableCollection<Image>(coin.Images ?? new List<Image>());
            SelectedImage = Images.FirstOrDefault();

            AddCoinImageCommand = new RelayCommand<WindowCommandContext>(AddCoinImage);
            RemoveCoinImageCommand = new RelayCommand<WindowCommandContext>(RemoveCoinImage);

            Validator.AddRule(() => Title, () => RuleResult.Assert(!string.IsNullOrWhiteSpace(Title), Resources.ErrorBlankField));
            Validator.AddRule(() => Country, () => RuleResult.Assert(Country != null, Resources.ErrorBlankField));
            Validator.AddRule(() => Currency, () => RuleResult.Assert(Currency != null, Resources.ErrorBlankField));

            Validator.ValidateAll();
        }

        public void AddImage(Image image)
        {
            image.Coin = _coin;
            Images.Add(image);
        }

        #region IDirtySerializable members
        
        public override IEntity GetEntity()
        {
            _coin.Images = Images.ToList();
            return _coin;
        }

        #endregion

    }
}
