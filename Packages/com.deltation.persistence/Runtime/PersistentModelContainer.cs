using DELTation.Persistence.Serialization;
using UnityEngine;

namespace DELTation.Persistence
{
    [RequireComponent(typeof(ModelSerializer))]
    public class PersistentModelContainer<T> : MonoBehaviour, IModelContainer<T> where T : class, new()
    {
        [SerializeField] private WriteIntervalType _writeIntervalType = WriteIntervalType.Frames;
        [SerializeField] private float _intervalSize = 1f;
        private T _cachedModel;
        private int _framesSinceLastWrite;

        private bool _saveScheduled;
        private float _secondsSinceLastWrite;

        private ModelSerializer _serializer;

        private bool ShouldWrite
        {
            get
            {
                if (!_saveScheduled) return false;

                switch (_writeIntervalType)
                {
                    case WriteIntervalType.Frames: return _framesSinceLastWrite >= _intervalSize;
                    case WriteIntervalType.Seconds: return _secondsSinceLastWrite >= _intervalSize;
                    default: return true;
                }
            }
        }

        private void Awake()
        {
            _serializer = GetComponent<ModelSerializer>();

            if (_serializer == null)
                Debug.LogError($"{name} requires a {typeof(ModelSerializer)}.");
            else
                _serializer.SetUp(typeof(T));
        }

        private void LateUpdate()
        {
            _framesSinceLastWrite += 1;
            _secondsSinceLastWrite += Time.deltaTime;
            if (!ShouldWrite) return;

            ForceSaveChanges();
        }

        private void OnDisable()
        {
            CleanUp();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (_serializer == null) return;
            if (!hasFocus)
                ForceSaveChanges();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (_serializer == null) return;
            if (pauseStatus)
                ForceSaveChanges();
        }

        private void OnApplicationQuit()
        {
            CleanUp();
        }

        public T Model
        {
            get
            {
                if (_cachedModel != null) return _cachedModel;

                if (_serializer.TryDeserialize(out var model))
                {
                    _cachedModel = (T) model;
                }
                else
                {
                    _cachedModel = new T();
                    _serializer.Serialize(_cachedModel);
                }

                return _cachedModel;
            }
            set => _cachedModel = value;
        }

        public void SaveChanges()
        {
            _saveScheduled = true;
        }

        private void CleanUp()
        {
            if (_serializer == null) return;

            ForceSaveChanges();

            if (_serializer.IsSetUp)
            {
                _serializer.Flush();
                _serializer = null;
            }
        }

        private void ForceSaveChanges()
        {
            _saveScheduled = false;
            _framesSinceLastWrite = 0;
            _secondsSinceLastWrite = 0;

            if (_cachedModel != null)
                _serializer.Serialize(_cachedModel);
        }

        private enum WriteIntervalType
        {
            Frames,
            Seconds,
        }
    }
}