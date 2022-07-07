using DELTation.Persistence.Building;
using UnityEngine;

namespace DELTation.Persistence
{
    public abstract class PersistentStateMonoBase<TModel> : MonoBehaviour where TModel : class
    {
        [SerializeField] private string _fileName = "save.txt";

        private PersistentState<TModel> _persistentState;

        public TModel Model => _persistentState.Model;

        protected void Awake()
        {
            try
            {
                var builder = PersistentStateBuilder<TModel>.Start();
                builder.WithFile(_fileName);
                ConstructPersistentState(builder);
                _persistentState = builder.Build();
            }
            catch
            {
                enabled = false;
                throw;
            }
        }

        protected void Update()
        {
            _persistentState.Update(Time.unscaledDeltaTime);
        }

        protected void OnDestroy()
        {
            _persistentState?.Dispose();
            _persistentState = null;
        }

        protected void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
                _persistentState?.BackupSave();
        }

        protected void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                _persistentState?.BackupSave();
        }

        public void ForceSave() => _persistentState.ForceSave();

        protected abstract void ConstructPersistentState(PersistentStateBuilder<TModel> builder);

        public void Save() => _persistentState.Save();
    }
}