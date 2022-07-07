using JetBrains.Annotations;

namespace DELTation.Persistence.Serializers
{
    public interface IPersistentSerializer<T>
    {
        void Init(string fileName);
        void Serialize(T model);

        void SetLogLevel(LogLevel logLevel);

        [CanBeNull]
        T Deserialize();
    }

    public enum LogLevel
    {
        None,
        Info,
        Warning,
        Error,
    }
}