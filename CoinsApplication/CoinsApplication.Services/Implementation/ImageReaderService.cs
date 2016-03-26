using System.IO;
using System.Windows.Media.Imaging;
using CoinsApplication.Misc;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Services.Implementation
{
    public class ImageReaderService : IImageReaderService
    {
        public byte[] ReadImage(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                return fileStream.ReadFully();
            }
        }
    }
}