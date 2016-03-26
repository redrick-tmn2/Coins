using System.Windows.Media.Imaging;
using CoinsApplication.DAL.Entities;

namespace CoinsApplication.Services.Interfaces.ImageService
{
    public interface ICachedImage
    {
        BitmapImage BitmapImage { get; set; }

        byte[] ImageBytes { get; set; }
    }
}