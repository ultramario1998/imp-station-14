using Content.Shared._Impstation.Trigger.Components.Triggers;
using Content.Shared.Power;
using Content.Shared.Power.Components;
using Content.Shared.Trigger.Systems;

namespace Content.Shared._Impstation.Trigger.Systems;

public sealed class TriggerOnBatteryFullSystem : EntitySystem
{
    [Dependency] private readonly TriggerSystem _trigger = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<TriggerOnBatteryFullComponent, PredictedBatteryChargeChangedEvent>(OnChargeChanged);
    }

    private void OnChargeChanged(Entity<TriggerOnBatteryFullComponent> ent, ref PredictedBatteryChargeChangedEvent args)
    {
        if (TryComp(ent.Owner, out PredictedBatteryComponent? battery) && battery.LastCharge >= battery.MaxCharge)
        {
            _trigger.Trigger(ent);
        }
    }
}
