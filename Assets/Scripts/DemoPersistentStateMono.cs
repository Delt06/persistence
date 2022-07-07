using DELTation.Persistence;
using DELTation.Persistence.Building;

public class DemoPersistentStateMono : PersistentStateMonoBase<DemoModel>
{
    protected override void ConstructPersistentState(PersistentStateBuilder<DemoModel> builder)
    {
        builder
            .WithJsonUtilitySerializer()
            .WithDefaultModelFallback()
            .WithPeriodicWrite(1f)
            ;
    }
}