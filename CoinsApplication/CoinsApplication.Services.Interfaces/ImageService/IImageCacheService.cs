namespace CoinsApplication.Services.Interfaces.ImageService
{
    public interface IImageCacheService
    {
        ICachedImage Get(object id);
        void Add(object id, ICachedImage image);
        void Remove(object id);
    }
}
