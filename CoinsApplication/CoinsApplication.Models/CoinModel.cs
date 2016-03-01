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

        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }
    }
}
