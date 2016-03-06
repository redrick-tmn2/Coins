using System.Windows.Media;

namespace CoinsApplication.Services.Interfaces
{
    public interface IImageReaderService
    {
        ImageSource ReadImage(string fileName);
    }
}
