using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.DAL.Repositories
{
    public interface ISaveObjectRepository
    {
        void Save(IEntity entity);
    }
}