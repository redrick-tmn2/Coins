using System.IO;
using System.Windows.Media;
using CoinsApplication.Services.Extensions;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services.Implementation.System
{
    public class ImageReaderService : IImageReaderService
    {
        public ImageSource ReadImage(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                return fileStream.ReadFully().ToImageSource();
            }
        }
    }
}