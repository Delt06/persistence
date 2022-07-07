namespace DELTation.Persistence.Timers
{
    public class PersistentFrameStateWriteTimer : IPersistentStateWriteTimer
    {
        private readonly int _framePeriod;
        private int _elapsedFrames;

        public PersistentFrameStateWriteTimer(int framePeriod) => _framePeriod = framePeriod;

        public bool WriteOnBackup => true;

        public void Update(float deltaTime, out bool shouldWrite)
        {
            _elapsedFrames++;
            if (_elapsedFrames >= _framePeriod)
            {
                _elapsedFrames %= _framePeriod;
                shouldWrite = true;
            }
            else
            {
                shouldWrite = false;
            }
        }
    }
}