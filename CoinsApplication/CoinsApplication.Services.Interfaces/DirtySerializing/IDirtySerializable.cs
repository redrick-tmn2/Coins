using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.Services.Interfaces.DirtySerializing
{
    public interface IDirtySerializable
    {
        bool IsDirty { get; set; }

        IEntity GetEntity();
    }
}
