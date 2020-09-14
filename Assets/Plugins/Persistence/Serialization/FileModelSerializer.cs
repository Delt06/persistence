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

			if (Stream.Length == 0)
			{
				ReportDeserializationError("The stream is empty.");
				model = default;
				return false;
			}
			
			try
			{
				model = DeserializeViaFormatter();
				return true;
			}
			catch (Exception e)
			{
				ReportDeserializationError(e);
				model = default;
				return false;
			}
		}

		private void ReportDeserializationError(object error)
		{
			OnDeserializationError(error);
		}

		protected abstract object DeserializeViaFormatter();
		
		protected override void SetUpProcedure(Type modelType)
		{
			Stream = new FileStream(FullPath, FileMode.OpenOrCreate);
		}

		private string FullPath => Path.Combine(Application.persistentDataPath, _fileName);

		protected override void FlushIfSetUp()
		{
			Stream.Flush();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Stream?.Dispose();
		}
		
		protected FileStream Stream { get; private set; }
	}
}