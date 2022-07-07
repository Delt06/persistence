using DELTation.Persistence;
using DELTation.Persistence.Building;

public class DemoPersistentStateMono : PersistentStateMonoBase<DemoModel>
{
    protected override void ConstructPersistentState(PersistentStateBuilder<DemoModel> builder)
    {
        builder
            .WithJsonUtilitySerializer()
            .WithDefaultModelFallback()
            .WritePeriodicWrite(1f)
            ;
    }
}