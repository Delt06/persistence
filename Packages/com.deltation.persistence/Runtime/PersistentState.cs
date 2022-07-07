using System;
using DELTation.Persistence.Serializers;
using DELTation.Persistence.Timers;
using JetBrains.Annotations;

namespace DELTation.Persistence
{
    public sealed class PersistentState<TModel> : IDisposable where TModel : class
    {
        private readonly Func<TModel> _modelFallback;
        private readonly IPersistentSerializer<TModel> _serializer;
        private readonly IPersistentStateWriteTimer _writeTimer;

        [CanBeNull]
        private TModel _cachedModel;
        private bool _isDirty;

        public PersistentState([NotNull] IPersistentSerializer<TModel> serializer, [NotNull] Func<TModel> modelFallback,
            [NotNull] IPersistentStateWriteTimer writeTimer, string fileName)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _modelFallback = modelFallback ?? throw new ArgumentNullException(nameof(modelFallback));
            _writeTimer = writeTimer ?? throw new ArgumentNullException(nameof(writeTimer));
            _serializer.Init(fileName);
        }


        public TModel Model
        {
            get
            {
                if (_cachedModel != null)
                    return _cachedModel;
                _cachedModel = _serializer.Deserialize();
                _cachedModel ??= _modelFallback();
                return _cachedModel;
            }
        }


        public void Dispose()
        {
            BackupSave();
            _isDirty = false;
            _cachedModel = null;
        }

        public void BackupSave()
        {
            if (_isDirty && _writeTimer.WriteOnBackup)
                ForceSave();
        }

        public void Update(float deltaTime)
        {
            _writeTimer.Update(deltaTime, out var shouldWrite);
            if (_isDirty && shouldWrite)
                ForceSave();
        }

        public void Save()
        {
            _isDirty = true;
        }

        public void ForceSave()
        {
            _isDirty = false;
            _serializer.Serialize(Model);
        }
    }
}