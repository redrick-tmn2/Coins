using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CoinsApplication.Services.ImageCaching;
using CoinsApplication.Services.Interfaces.ImageCaching;
using CoinsApplication.Services.Interfaces.Logging;
using Microsoft.Practices.ServiceLocation;

namespace CoinsApplication.Converters
{
    public class CachingImageConverter : IValueConverter
    {
        protected ILoggingService LoggingService => ServiceLocator.Current.GetInstance<ILoggingService>();

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
            try
            {
                var imageBytes = value as byte[];
                if (imageBytes == null)
                {
                    return null;
                }

                var image = GetOrCreateCachedImage(imageBytes);

                return image.BitmapImage;
            }
            catch (Exception ex)
            {
                LoggingService.Error("Unable to convert image.", ex);
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}