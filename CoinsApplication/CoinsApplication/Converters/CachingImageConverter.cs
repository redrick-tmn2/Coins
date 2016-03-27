using System;
using System.Globalization;
using System.Windows.Data;
using CoinsApplication.Services.ImageService;
using CoinsApplication.Services.Interfaces.ImageCaching;
using Microsoft.Practices.ServiceLocation;

namespace CoinsApplication.Converters
{
    public class CachingImageConverter : IValueConverter
    {
        protected IImageCacheService ImageCacheService => ServiceLocator.Current.GetInstance<IImageCacheService>();

        protected ICachedImage GetOrCreateCachedImage(byte[] imageBytes)
        {
            var image = ImageCacheService.Get(imageBytes);

            if (image != null)
            {
                return image;
            }

            image = new CachedImage(imageBytes);
            ImageCacheService.Add(imageBytes, image);

            return image;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageBytes = value as byte[];
            if (imageBytes == null)
            {
                return null;
            }

            var image = GetOrCreateCachedImage(imageBytes);

            return image.BitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}