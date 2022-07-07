namespace DELTation.Persistence.Timers
{
    public interface IPersistentStateWriteTimer
    {
        bool WriteOnBackup { get; }
        void Update(float deltaTime, out bool shouldWrite);
    }
}