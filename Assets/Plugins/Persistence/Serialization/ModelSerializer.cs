using System;
using UnityEngine;

namespace Persistence.Serialization
{
	public abstract class ModelSerializer : MonoBehaviour, IModelSerializer
	{
		public void SetUp(Type modelType)
		{
			if (IsSetUp) return;

			SetUpProcedure(modelType);
			IsSetUp = true;
		}

		protected abstract void SetUpProcedure(Type modelType);
		public bool IsSetUp { get; private set; }

		public void Serialize(object model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			RequireSetUp();
			SerializeIfSetUp(model);
		}

		protected abstract void SerializeIfSetUp(object model);

		public bool TryDeserialize(out object model)
		{
			RequireSetUp();
			return TryDeserializeIfSetUp(out model);
		}

		protected abstract bool TryDeserializeIfSetUp(out object model);

		public void Flush()
		{
			RequireSetUp();
			FlushIfSetUp();
		}

		protected abstract void FlushIfSetUp();

		protected virtual void RequireSetUp()
		{
			if (!IsSetUp)
			{
				throw new InvalidOperationException("Not set up.");
			}
		}

		protected virtual void OnDestroy() { }
	}
}