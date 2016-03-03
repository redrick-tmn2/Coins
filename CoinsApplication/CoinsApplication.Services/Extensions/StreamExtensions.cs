using System.IO;

namespace CoinsApplication.Services.Extensions
{
    public static class StreamExtensions
    {
        private const int BufferSize = 16 * 1024;

        public static byte[] ReadFully(this Stream input)
        {
            var buffer = new byte[BufferSize];
            using (var memoryStream = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, read);
                }

                return memoryStream.ToArray();
            }
        }
    }
}
