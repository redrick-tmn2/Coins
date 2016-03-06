using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace CoinsApplication.Services.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image image)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }

        public static BitmapImage ToImageSource(this byte[] byteArrayImage)
        {
            if (byteArrayImage != null && byteArrayImage.Length > 0)
            {
                var memoryStream = new MemoryStream(byteArrayImage);

                var bitmapImage = new BitmapImage();

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();

                return bitmapImage;
            }

            return null;
        }

        public static BitmapImage ToImageSource(this Image image)
        {
            return image.ToByteArray().ToImageSource();
        }
    }
}
