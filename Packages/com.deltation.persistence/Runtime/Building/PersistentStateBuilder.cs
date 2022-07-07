using System;
using DELTation.Persistence.Serializers;
using DELTation.Persistence.Timers;
using JetBrains.Annotations;

namespace DELTation.Persistence.Building
{
    public class PersistentStateBuilder<TModel> where TModel : class
    {
        [CanBeNull]
        private string _fileName;
        private LogLevel? _logLevel;
        [CanBeNull]
        private Func<TModel> _modelFallback;
        [CanBeNull]
        private IPersistentSerializer<TModel> _serializer;
        [CanBeNull]
        private IPersistentStateWriteTimer _writeTimer;

        private PersistentStateBuilder() { }

        public PersistentStateBuilder<TModel> WithSerializer([NotNull] IPersistentSerializer<TModel> serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            return this;
        }

        public PersistentStateBuilder<TModel> WithModelFallback([NotNull] Func<TModel> modelFactory)
        {
            _modelFallback = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            return this;
        }

        public PersistentStateBuilder<TModel> WithWriteTimer([NotNull] IPersistentStateWriteTimer writeTimer)
        {
            _writeTimer = writeTimer ?? throw new ArgumentNullException(nameof(writeTimer));
            return this;
        }

        public PersistentStateBuilder<TModel> WithFile([NotNull] string fileName)
        {
            _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            return this;
        }

        public PersistentStateBuilder<TModel> SetLogLevel(LogLevel logLevel)
        {
            _logLevel = logLevel;
            return this;
        }

        public PersistentState<TModel> Build()
        {
            if (_serializer == null)
                throw new InvalidOperationException("Serializer is not set.");
            if (_modelFallback == null)
                throw new InvalidOperationException("Model fallback is not set.");
            if (_writeTimer == null)
                throw new InvalidOperationException("Write timer is not set.");
            if (_fileName == null)
                throw new InvalidOperationException("File name is not set.");

            if (_logLevel != null)
                _serializer.SetLogLevel(_logLevel.Value);

            return new PersistentState<TModel>(_serializer, _modelFallback, _writeTimer, _fileName);
        }

        public static PersistentStateBuilder<TModel> Start() => new PersistentStateBuilder<TModel>();
    }
}