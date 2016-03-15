using System;
using System.Collections.Generic;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.Repositories;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.Services.Interfaces.Utils;

namespace CoinsApplication.Services.Implementation
{
    public class DirtySerializableCacheService : IDirtySerializableCacheService
    {
        private readonly ISaveObjectRepository _saveObjectRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly HashSet<IDirtySerializable> _serializable = new HashSet<IDirtySerializable>();

        public bool IsEmpty => _serializable.Count == 0;

        public event EventHandler CacheChanged;

        public DirtySerializableCacheService(ISaveObjectRepository saveObjectRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (saveObjectRepository == null)
                throw new ArgumentNullException(nameof(saveObjectRepository));

            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));

            _saveObjectRepository = saveObjectRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Add(IDirtySerializable serializable)
        {
            _serializable.Add(serializable);
            OnCacheChanged();
        }

        public void SaveAll()
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                foreach (var dirtySerializable in _serializable)
                {
                    _saveObjectRepository.Save(dirtySerializable.GetEntity());
                    dirtySerializable.IsDirty = false;
                }

                unitOfWork.Commit();

                _serializable.Clear();
                OnCacheChanged();
            }
        }

        private void OnCacheChanged()
        {
            CacheChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}