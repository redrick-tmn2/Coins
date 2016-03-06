using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace CoinsApplication.Models
{
    public class CoinModel : ObservableObject
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set { Set(ref _year, value); }
        }

        private CountryModel _country;
        public CountryModel Country
        {
            get { return _country; }
            set { Set(ref _country, value); }
        }

        private CurrencyModel _currency;
        public CurrencyModel Currency
        {
            get { return _currency; }
            set { Set(ref _currency, value); }
        }
    }
}
