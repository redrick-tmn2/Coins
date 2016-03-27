using System;
using System.Collections.Generic;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.DAL.Repositories;
using CoinsApplication.Services.Interfaces.DirtySerializing;

namespace CoinsApplication.Services.Implementation
{
    public class DirtySerializableCacheService : IDirtySerializableCacheService
    {
        private readonly ISaveObjectRepository _saveObjectRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly HashSet<IDirtySerializable> _saveCollection = new HashSet<IDirtySerializable>();
        private readonly HashSet<IDirtySerializable> _removeCollection = new HashSet<IDirtySerializable>();

        public bool IsEmpty => _saveCollection.Count == 0 && _removeCollection.Count == 0;

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
            _saveCollection.Add(serializable);
            OnCacheChanged();
        }

        public void Remove(IDirtySerializable serializable)
        {
            _saveCollection.Remove(serializable);
            _removeCollection.Add(serializable);

            OnCacheChanged();
        }

        public void Commit()
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                foreach (var dirtySerializable in _removeCollection)
                {
                    var entity = dirtySerializable.GetEntity();
                    _saveObjectRepository.Remove(entity);
                    dirtySerializable.IsDirty = false;
                }

                foreach (var dirtySerializable in _saveCollection)
                {
                    var entity = dirtySerializable.GetEntity();
                    _saveObjectRepository.Save(entity);
                    dirtySerializable.IsDirty = false;
                }

                unitOfWork.Commit();

                _saveCollection.Clear();
                _removeCollection.Clear();
                OnCacheChanged();
            }
        }

        private void OnCacheChanged()
        {
            CacheChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}