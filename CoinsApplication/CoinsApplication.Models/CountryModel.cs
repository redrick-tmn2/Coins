using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace CoinsApplication.Models
{
    public class CountryModel : ObservableObject
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
    }
}