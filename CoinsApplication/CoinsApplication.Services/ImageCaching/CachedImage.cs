using System.Windows.Media.Imaging;
using CoinsApplication.Misc;
using CoinsApplication.Services.Interfaces.ImageCaching;

namespace CoinsApplication.Services.ImageService
{
    public class CachedImage : ICachedImage
    {
        public BitmapImage BitmapImage { get; set; }
        public byte[] ImageBytes { get; set; }

        public CachedImage(byte[] imageBytes)
        {
            ImageBytes = imageBytes;
            BitmapImage = imageBytes.ToImageSource();
        }
    }
}