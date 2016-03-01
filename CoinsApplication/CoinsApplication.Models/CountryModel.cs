using GalaSoft.MvvmLight;

namespace CoinsApplication.Models
{
    public class CountryModel : ObservableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }
    }
}