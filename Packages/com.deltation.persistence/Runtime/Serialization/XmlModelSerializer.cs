using System;
using System.Xml.Serialization;

namespace Persistence.Serialization
{
	public sealed class XmlModelSerializer : FileModelSerializer
	{
		protected override void SerializeViaFormatter(object model)
		{
			_formatter.Serialize(Stream, model);
		}

		protected override object DeserializeViaFormatter()
		{
			return _formatter.Deserialize(Stream);
		}

		protected override void SetUpProcedure(Type modelType)
		{
			base.SetUpProcedure(modelType);
			_formatter = new XmlSerializer(modelType);
		}

		private XmlSerializer _formatter;
	}
}