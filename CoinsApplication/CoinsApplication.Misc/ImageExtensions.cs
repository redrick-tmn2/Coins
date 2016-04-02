using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace CoinsApplication.Misc
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
                var bitmapImage = new BitmapImage();
                var memoryStream = new MemoryStream(byteArrayImage);

                
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnDemand;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                

                return bitmapImage;
            }

            return null;
        }

        public static BitmapImage ToImageSource(this Image image)
        {
            return image.ToByteArray().ToImageSource();
        }

        public static byte[] ToByteArray(this BitmapImage image)
        {
            if (image == null)
            {
                return null;
            }

            using (MemoryStream memStream = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(memStream);
                return memStream.ToArray();
            }
        }
    }
}
