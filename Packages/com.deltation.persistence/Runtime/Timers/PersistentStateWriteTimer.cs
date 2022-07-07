namespace DELTation.Persistence.Timers
{
    public class PersistentStateWriteTimer : IPersistentStateWriteTimer
    {
        private readonly float _period;
        private float _timer;

        public PersistentStateWriteTimer(float period) => _period = period;

        public bool WriteOnBackup => true;

        public void Update(float deltaTime, out bool shouldWrite)
        {
            _timer += deltaTime;
            if (_timer >= _period)
            {
                _timer %= _period;
                shouldWrite = true;
            }
            else
            {
                shouldWrite = false;
            }
        }
    }
}