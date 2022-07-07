using UnityEngine;

namespace DELTation.Persistence.Serializers
{
    public sealed class PersistentJsonUtilitySerializer<T> : PersistentSerializerBase<T>
    {
        public override void Serialize(T model)
        {
            WriteAllText(JsonUtility.ToJson(model));
        }

        public override T Deserialize() => JsonUtility.FromJson<T>(ReadAllText());
    }
}