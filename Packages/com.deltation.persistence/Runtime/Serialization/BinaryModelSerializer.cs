using System.Runtime.Serialization.Formatters.Binary;

namespace DELTation.Persistence.Serialization
{
	public sealed class BinaryModelSerializer : FileModelSerializer
	{
		protected override void SerializeViaFormatter(object model)
		{
			_formatter.Serialize(Stream, model);
		}

		protected override object DeserializeViaFormatter()
		{
			return _formatter.Deserialize(Stream);
		}
		
		private readonly BinaryFormatter _formatter = new BinaryFormatter();
	}
}