using System;
using System.IO;
using UnityEngine;

namespace Persistence.Serialization
{
	public abstract class FileModelSerializer : ModelSerializer
	{
		[SerializeField] private string _fileName = "file.save";

		protected override void SerializeIfSetUp(object model)
		{
			Stream.Position = 0;
			SerializeViaFormatter(model);
		}

		protected abstract void SerializeViaFormatter(object model);

		protected sealed override bool TryDeserializeIfSetUp(out object model)
		{
			Stream.Position = 0;
			
			try
			{
				model = DeserializeViaFormatter();
				return true;
			}
			catch (Exception e)
			{
				Debug.LogWarning("Could not deserialize model because:");
				Debug.LogWarning(e);
				model = default;
				return false;
			}
		}

		protected abstract object DeserializeViaFormatter();
		
		protected override void SetUpProcedure(Type modelType)
		{
			Stream = new FileStream(FullPath, FileMode.OpenOrCreate);
		}
		
		protected override void FlushIfSetUp()
		{
			Stream.Flush();
		}
		
		private string FullPath => Path.Combine(Application.persistentDataPath, _fileName);

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Stream?.Dispose();
		}
		
		protected Stream Stream { get; private set; }
	}
}