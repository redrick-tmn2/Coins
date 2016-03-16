using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.Services.Interfaces.Utils
{
    public interface IDirtySerializable
    {
        bool IsDirty { get; set; }

        IEntity GetEntity();
    }
}
