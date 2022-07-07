using System;
using System.Xml.Serialization;

namespace DELTation.Persistence.Serialization
{
    public sealed class XmlModelSerializer : FileModelSerializer
    {
        private XmlSerializer _formatter;

        protected override void SerializeViaFormatter(object model)
        {
            _formatter.Serialize(Stream, model);
        }

        protected override object DeserializeViaFormatter() => _formatter.Deserialize(Stream);

        protected override void SetUpProcedure(Type modelType)
        {
            base.SetUpProcedure(modelType);
            _formatter = new XmlSerializer(modelType);
        }
    }
}