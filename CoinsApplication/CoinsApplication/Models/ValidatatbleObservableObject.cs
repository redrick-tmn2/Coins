using System;
using System.ComponentModel;
using CoinsApplication.Services.Interfaces.DirtySerializing;
using MvvmValidation;

namespace CoinsApplication.Models
{
    [Flags]
    public enum SetFlag
    {
        None = 0,
        Dirty = 0x1,
        Validate = 0x2,
    }

    public abstract class ValidatatbleObservableObject : DirtyObservableObject, IDataErrorInfo
    {
        public ValidationHelper Validator { get; }

        private DataErrorInfoAdapter DataErrorInfoAdapter { get; }

        public string this[string columnName] => DataErrorInfoAdapter[columnName];

        public string Error => DataErrorInfoAdapter.Error;

        protected ValidatatbleObservableObject(IDirtySerializableCacheService serializableCacheService) 
            : base(serializableCacheService)
        {
            Validator = new ValidationHelper();
            DataErrorInfoAdapter = new DataErrorInfoAdapter(Validator);
        }
    }
}