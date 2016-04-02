using System;
using System.Windows.Media.Imaging;

namespace CoinsApplication.Services.Interfaces.ImageCaching
{
    public interface ICachedImage : IDisposable
    {
        BitmapImage BitmapImage { get; set; }

        byte[] ImageBytes { get; set; }
    }
}