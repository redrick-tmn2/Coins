using System.IO;
using System.Windows.Media.Imaging;
using CoinsApplication.Misc;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services.Implementation.System
{
    public class ImageReaderService : IImageReaderService
    {
        public BitmapImage ReadImage(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                return fileStream.ReadFully().ToImageSource();
            }
        }
    }
}