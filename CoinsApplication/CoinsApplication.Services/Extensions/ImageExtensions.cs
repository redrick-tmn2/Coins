using System.Drawing;

namespace CoinsApplication.Services.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image image)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }
    }
}
