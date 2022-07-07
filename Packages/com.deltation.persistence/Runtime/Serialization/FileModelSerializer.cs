using System;
using System.IO;
using UnityEngine;

namespace DELTation.Persistence.Serialization
{
    public abstract class FileModelSerializer : ModelSerializer
    {
        [SerializeField] private string _fileName = "file.save";

        private string FullPath => Path.Combine(PersistenceUtils.SavesPath, _fileName);

        protected FileStream Stream { get; private set; }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Stream?.Dispose();
        }

        protected override void SerializeIfSetUp(object model)
        {
            Stream.Position = 0;
            Stream.SetLength(0);
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

        protected override void FlushIfSetUp()
        {
            Stream.Flush();
        }
    }
}