using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CoinsApplication.Converters
{
    [ValueConversion(typeof(byte[]), typeof(ImageSource))]
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var byteArrayImage = value as byte[];
 
            if (byteArrayImage != null && byteArrayImage.Length > 0)
            {
                var memoryStream = new MemoryStream(byteArrayImage);
 
                var bitmapImage = new BitmapImage();
 
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();

                return bitmapImage;
            }
 
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
