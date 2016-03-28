using System;
using CoinsApplication.DAL.Entities;
using CoinsApplication.Services.Interfaces.DirtySerializing;

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
            var result = new CoinModel(_serializableCacheService)
            {
                Year = DateTime.Now.Year
            };

            _serializableCacheService.Add(result);

            return result;
        }
    }
}
