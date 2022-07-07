using System;
using UnityEngine;

namespace DELTation.Persistence.Serialization
{
    [RequireComponent(typeof(IModelSerializer))]
    public sealed class ModelSerializationLog : MonoBehaviour
    {
        [SerializeField] private LogLevel _logLevel = LogLevel.Error;
        private EventHandler<object> _onDeserializationError;

        private IModelSerializer _serializer;

        private void Awake()
        {
            _serializer = GetComponent<IModelSerializer>();

            if (_serializer == null)
                Debug.LogError($"{name} requires {nameof(IModelSerializer)}.");

            _onDeserializationError = (sender, error) =>
            {
                Log("Cannot not deserialize model because:");
                Log(error);
            };
        }

        private void OnEnable()
        {
            _serializer.DeserializationError += _onDeserializationError;
        }

        private void OnDisable()
        {
            _serializer.DeserializationError -= _onDeserializationError;
        }

        private void Log(object message)
        {
            switch (_logLevel)
            {
                case LogLevel.Default:
                    Debug.Log(message, this);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(message, this);
                    break;
                case LogLevel.Error:
                    Debug.LogError(message, this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_logLevel));
            }
        }

        private enum LogLevel
        {
            Default,
            Warning,
            Error,
        }
    }
}