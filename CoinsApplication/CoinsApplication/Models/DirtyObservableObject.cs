using System;
using System.Runtime.CompilerServices;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.Services.Interfaces;
using GalaSoft.MvvmLight;

namespace CoinsApplication.Models
{
    public abstract class DirtyObservableObject : ObservableObject, IDirtySerializable
    {
        protected IDirtySerializableCacheService SerializableCacheService { get; }

        protected DirtyObservableObject(IDirtySerializableCacheService serializableCacheService)
        {
            if (serializableCacheService == null)
                throw new ArgumentNullException(nameof(serializableCacheService));

            SerializableCacheService = serializableCacheService;
        }

        public void SetAndDirty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Set(propertyName, ref field, newValue))
            {
                SetDirty();
            }
        }

        public void SetAndDirty<T>(T oldValue, T newValue, Action setValue, [CallerMemberName] string propertyName = null)
        {
            if (newValue != null && !newValue.Equals(oldValue))
            {
                setValue();
                RaisePropertyChanged(propertyName);

                SetDirty();
            }
        }

        public void SetDirty()
        {
            IsDirty = true;
            SerializableCacheService.Add(this);
        }

        public bool IsDirty { get; set; }

        public abstract IEntity GetEntity();
    }
}