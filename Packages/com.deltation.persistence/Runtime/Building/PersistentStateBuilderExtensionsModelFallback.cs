using System;
using JetBrains.Annotations;

namespace DELTation.Persistence.Building
{
    public static class PersistentStateBuilderExtensionsModelFallback
    {
        public static PersistentStateBuilder<TModel> WithDefaultModelFallback<TModel>(
            [NotNull] this PersistentStateBuilder<TModel> builder) where TModel : class, new()
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            return builder.WithModelFallback(() => new TModel());
        }
    }
}