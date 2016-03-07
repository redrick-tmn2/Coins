using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CoinsApplication.Models;
using CoinsApplication.ViewModel;

namespace CoinsApplication.Converters
{
    public class VeiwModelsToModelsConverter<T> : IValueConverter
        where T : class
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = value as IEnumerable<SelectableViewModelBase<T>>;
            if (collection == null)
            {
                return DependencyProperty.UnsetValue;
            }
            
            return collection.Select(x => x.Model);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CurrencyVeiwModelsToCurrencyModelsConverter : VeiwModelsToModelsConverter<CurrencyModel>
    {
    }

    public class CountryVeiwModelsToCountryModelsConverter : VeiwModelsToModelsConverter<CountryModel>
    {
    }
}
