using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.Services.Interfaces
{
    public interface IDirtySerializable
    {
        bool IsDirty { get; set; }

        IEntity GetEntity();
    }
}
