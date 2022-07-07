using System;
using JetBrains.Annotations;

namespace DELTation.Persistence.Serialization
{
    public interface IModelSerializer
    {
        bool IsSetUp { get; }
        void SetUp(Type modelType);

        void Serialize([NotNull] object model);

        bool TryDeserialize(out object model);

        void Flush();

        event EventHandler<object> DeserializationError;
    }
}