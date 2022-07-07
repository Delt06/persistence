using System;
using System.IO;
using UnityEngine;

namespace DELTation.Persistence.Serializers
{
    public abstract class PersistentSerializerBase<T> : IPersistentSerializer<T>
    {
        private string _filePath;
        private LogLevel _logLevel = LogLevel.Error;

        public void Init(string fileName)
        {
            _filePath = Path.Combine(PersistenceUtils.SavesPath, fileName);
        }

        public abstract void Serialize(T model);

        public void SetLogLevel(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public abstract T Deserialize();

        protected void WriteAllText(string text)
        {
            try
            {
                File.WriteAllText(_filePath, text);
            }
            catch (Exception e)
            {
                Log(e);
            }
        }

        private void Log(Exception e)
        {
            switch (_logLevel)
            {
                case LogLevel.Error:
                    Debug.LogError(e);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(e);
                    break;
                case LogLevel.Info:
                    Debug.Log(e);
                    break;
                case LogLevel.None:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected string ReadAllText()
        {
            try
            {
                return File.Exists(_filePath) ? File.ReadAllText(_filePath) : string.Empty;
            }
            catch (Exception e)
            {
                Log(e);
                return string.Empty;
            }
        }
    }
}