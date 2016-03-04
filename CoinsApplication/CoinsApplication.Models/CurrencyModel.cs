using GalaSoft.MvvmLight;

namespace CoinsApplication.Models
{
    public class CurrencyModel : ObservableObject
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { Set(ref _code, value); }
        }
    }
}
