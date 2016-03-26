using System.Windows.Media.Imaging;

namespace CoinsApplication.Services.Interfaces
{
    public interface IImageReaderService
    {
        byte[] ReadImage(string fileName);
    }
}
