using System;
using UnityEngine;

namespace DELTation.Persistence.Serialization
{
    public abstract class ModelSerializer : MonoBehaviour, IModelSerializer
    {
        protected virtual void OnDestroy() { }

        public void SetUp(Type modelType)
        {
            if (IsSetUp) return;

            SetUpProcedure(modelType);
            IsSetUp = true;
        }

        public bool IsSetUp { get; private set; }

        public void Serialize(object model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            RequireSetUp();
            SerializeIfSetUp(model);
        }

        public bool TryDeserialize(out object model)
        {
            RequireSetUp();
            return TryDeserializeIfSetUp(out model);
        }

        public void Flush()
        {
            RequireSetUp();
            FlushIfSetUp();
        }

        public event EventHandler<object> DeserializationError;

        protected abstract void SetUpProcedure(Type modelType);

        protected abstract void SerializeIfSetUp(object model);

        protected abstract bool TryDeserializeIfSetUp(out object model);

        protected abstract void FlushIfSetUp();

        protected virtual void RequireSetUp()
        {
            if (!IsSetUp) throw new InvalidOperationException("Not set up.");
        }

        protected virtual void OnDeserializationError(object error)
        {
            DeserializationError?.Invoke(this, error);
        }
    }
}