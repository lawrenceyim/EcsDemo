using System.Collections.Generic;
using EntityComponentSystem;

// Change to Health, Shield, OverHealth, OverShield, etc. and make them all damageable by different sources?
public class DamageableComponent : IComponent {
    private List<DamageSource> _damageableBySources;

    public DamageableComponent(List<DamageSource> damageableBySources) {
        _damageableBySources = damageableBySources;
    }

    public bool DamageableBy(DamageSource damageSource) {
        return _damageableBySources.Contains(damageSource);
    }
}