using System.Windows.Media.Imaging;
using CoinsApplication.Misc;
using CoinsApplication.Services.Interfaces.ImageService;

namespace CoinsApplication.Services.ImageCaching
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