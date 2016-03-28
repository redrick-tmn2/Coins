using CoinsApplication.Services.ImageCaching;
using CoinsApplication.Services.Interfaces.ImageCaching;

namespace CoinsApplication.Extensions
{
    public static class ImageExtensions
    {
        public static ICachedImage GetOrCreateCachedImage(this byte[] imageBytes, IImageCacheService imageCacheService)
        {
            var image = imageCacheService.Get(imageBytes);

            if (image != null)
            {
                return image;
            }

            image = new CachedImage(imageBytes);
            imageCacheService.Add(imageBytes, image);

            return image;
        }
    }
}
