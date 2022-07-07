using System.Runtime.Serialization.Formatters.Binary;

namespace DELTation.Persistence.Serialization
{
    public sealed class BinaryModelSerializer : FileModelSerializer
    {
        private readonly BinaryFormatter _formatter = new BinaryFormatter();

        protected override void SerializeViaFormatter(object model)
        {
            _formatter.Serialize(Stream, model);
        }

        protected override object DeserializeViaFormatter() => _formatter.Deserialize(Stream);
    }
}