using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CoinsApplication.Services.Interfaces
{
    public interface IImageReaderService
    {
        BitmapImage ReadImage(string fileName);
    }
}
