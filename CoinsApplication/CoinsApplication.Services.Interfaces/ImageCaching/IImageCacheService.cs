namespace CoinsApplication.Services.Interfaces.ImageCaching
{
    public interface IImageCacheService
    {
        bool IsCached(object id);

        ICachedImage Get(object id);

        void Add(object id, ICachedImage image);

        void Remove(object id);
    }
}
