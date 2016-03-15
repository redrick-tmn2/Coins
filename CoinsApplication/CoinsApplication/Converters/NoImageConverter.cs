using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CoinsApplication.Misc;

namespace CoinsApplication.Converters
{
    public class NoImageConverter : IValueConverter
    {
        private static readonly ImageSource NoImage = Properties.Resources.NoImage.ToImageSource();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return NoImage;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}