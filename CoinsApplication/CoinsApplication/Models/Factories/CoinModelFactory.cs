using System;
using CoinsApplication.DAL.Entities;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Models.Factories
{
    public class CoinModelFactory : ICoinModelFactory
    {
        private readonly IDirtySerializableCacheService _serializableCacheService;

        public CoinModelFactory(IDirtySerializableCacheService serializableCacheService)
        {
            if (serializableCacheService == null)
                throw new ArgumentNullException(nameof(serializableCacheService));

            _serializableCacheService = serializableCacheService;
        }

        public CoinModel Create(Coin coin)
        {
            return new CoinModel(coin, _serializableCacheService);
        }

        public CoinModel Create()
        {
            return new CoinModel(_serializableCacheService);
        }
    }
}
