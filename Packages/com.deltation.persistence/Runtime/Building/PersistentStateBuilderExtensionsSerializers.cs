using System;
using DELTation.Persistence.Serializers;
using JetBrains.Annotations;

namespace DELTation.Persistence.Building
{
    public static class PersistentStateBuilderExtensionsSerializers
    {
        public static PersistentStateBuilder<TModel> WithJsonUtilitySerializer<TModel>(
            [NotNull] this PersistentStateBuilder<TModel> builder) where TModel : class
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            return builder.WithSerializer(new PersistentJsonUtilitySerializer<TModel>());
        }
    }
}