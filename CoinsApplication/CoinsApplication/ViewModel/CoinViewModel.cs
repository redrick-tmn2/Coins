using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel
{
    public class CoinViewModel : ViewModelBase
    {
        private BitmapSource _image;
        private string _title;

        public string Title 
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        public BitmapSource Image 
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }
    }
}
