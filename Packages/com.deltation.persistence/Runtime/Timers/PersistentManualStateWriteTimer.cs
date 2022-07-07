namespace DELTation.Persistence.Timers
{
    public class PersistentManualStateWriteTimer : IPersistentStateWriteTimer
    {
        public bool WriteOnBackup => false;

        public void Update(float deltaTime, out bool shouldWrite)
        {
            shouldWrite = false;
        }
    }
}