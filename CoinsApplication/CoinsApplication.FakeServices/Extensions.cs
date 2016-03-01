using System.Drawing;

namespace CoinsApplication.FakeServices
{
    public static class Extensions
    {
        public static byte[] ToByteArray(this Image image)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }
    }
}
