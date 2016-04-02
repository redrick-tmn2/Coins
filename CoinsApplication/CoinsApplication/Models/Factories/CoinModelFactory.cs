using System;
using CoinsApplication.DAL.Entities;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.Services.Interfaces.DirtySerializing;
using CoinsApplication.Services.Interfaces.ImageCaching;

namespace CoinsApplication.Models.Factories
{
    public class CoinModelFactory : ICoinModelFactory
    {
        private readonly IDirtySerializableCacheService _serializableCacheService;
        private readonly IImageReaderService _imageReaderService;
        private readonly IDialogService _dialogService;
        private readonly IImageCacheService _imageCacheService;

        public CoinModelFactory(IDirtySerializableCacheService serializableCacheService,
            IDialogService dialogService,
            IImageReaderService imageReaderService,
            IImageCacheService imageCacheService)
        {
            if (serializableCacheService == null)
                throw new ArgumentNullException(nameof(serializableCacheService));
            if (dialogService == null)
                throw new ArgumentNullException(nameof(dialogService));
            if (imageReaderService == null)
                throw new ArgumentNullException(nameof(imageReaderService));
            if (imageCacheService == null)
                throw new ArgumentNullException(nameof(imageCacheService));

            _serializableCacheService = serializableCacheService;
            _dialogService = dialogService;
            _imageReaderService = imageReaderService;
            _imageCacheService = imageCacheService;
        }

        public CoinModel Create(Coin coin)
        {
            return new CoinModel(coin, _serializableCacheService, _dialogService, _imageReaderService, _imageCacheService);
        }

        public CoinModel Create()
        {
            var result = new CoinModel(new Coin(), _serializableCacheService, _dialogService, _imageReaderService, _imageCacheService)
            {
                Year = DateTime.Now.Year
            };

            _serializableCacheService.Add(result);

            return result;
        }
    }
}
