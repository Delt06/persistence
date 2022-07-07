using System;
using DELTation.Persistence.Timers;
using JetBrains.Annotations;

namespace DELTation.Persistence.Building
{
    public static class PersistentStateBuilderExtensionsWriteTimers
    {
        public static PersistentStateBuilder<TModel> WithManualWrite<TModel>(
            [NotNull] this PersistentStateBuilder<TModel> builder) where TModel : class
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            return builder.WithWriteTimer(new PersistentManualStateWriteTimer());
        }

        public static PersistentStateBuilder<TModel> WithPeriodicWriteInFrames<TModel>(
            [NotNull] this PersistentStateBuilder<TModel> builder, int framePeriod) where TModel : class
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (framePeriod <= 0) throw new ArgumentOutOfRangeException(nameof(framePeriod));
            return builder.WithWriteTimer(new PersistentFrameStateWriteTimer(framePeriod));
        }

        public static PersistentStateBuilder<TModel> WithPeriodicWrite<TModel>(
            [NotNull] this PersistentStateBuilder<TModel> builder, float timePeriod) where TModel : class
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (timePeriod <= 0) throw new ArgumentOutOfRangeException(nameof(timePeriod));
            return builder.WithWriteTimer(new PersistentStateWriteTimer(timePeriod));
        }
    }
}