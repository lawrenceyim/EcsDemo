using System.Collections.Generic;
using EntityComponentSystem;

public class DamageEvent : IEvent {
    public ulong SourceEntityId { get; }
    public ulong TargetEntityId { get; }
    public double MultiHit { get; }
    public double CriticalChance { get; }
    public double CriticalDamageMultiplier { get; }
    public double StatusChance { get; }
    public List<DamageInstance> DamageInstances { get; }

    public DamageEvent(ulong sourceEntityId, ulong targetEntityId, double multiHit, double criticalChance, double criticalDamageMultiplier, double statusChance, List<DamageInstance> damageInstances) {
        SourceEntityId = sourceEntityId;
        TargetEntityId = targetEntityId;
        MultiHit = multiHit;
        CriticalChance = criticalChance;
        CriticalDamageMultiplier = criticalDamageMultiplier;
        StatusChance = statusChance;
        DamageInstances = damageInstances;
    }
}

public record DamageInstance(DamageType DamageType, double DamageAmount);