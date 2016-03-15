using System;
using CoinsApplication.Services.Interfaces.Utils;

namespace CoinsApplication.Services.Interfaces
{
    public interface IDirtySerializableCacheService
    {
        bool IsEmpty { get; }

        void Add(IDirtySerializable serializable);

        void SaveAll();

        event EventHandler CacheChanged;
    }
}
