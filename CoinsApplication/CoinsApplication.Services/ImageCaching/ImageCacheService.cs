using System.Collections.Generic;
using CoinsApplication.Services.Interfaces.ImageCaching;

namespace CoinsApplication.Services.ImageCaching
{
    public class ImageCacheService : IImageCacheService
    {
        private readonly IDictionary<object, ICachedImage> _cache = new Dictionary<object, ICachedImage>();

        public ICachedImage Get(object id)
        {
            ICachedImage result;
            return _cache.TryGetValue(id, out result) ? result : null;
        }

        public void Add(object id, ICachedImage image)
        {
            _cache[id] = image;
        }

        public void Remove(object id)
        {
            ICachedImage cachedImage;
            if (!_cache.TryGetValue(id, out cachedImage))
            {
                return;
            }
            
            _cache.Remove(id);
        }
    }
}
