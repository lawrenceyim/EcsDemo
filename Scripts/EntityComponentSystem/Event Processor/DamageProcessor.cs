using EntityComponentSystem;

public class DamageProcessor : IEventProcessor<DamageEvent> {
    private EntityManager _entityManager;

    public DamageProcessor(EntityManager entityManager) {
        _entityManager = entityManager;
    }

    public void ProcessEvent(DamageEvent damageEvent) {
        Entity target = _entityManager.GetEntity(damageEvent.TargetEntityId);

        if (target is null) {
            return;
        }

        // TODO: calculate number of hits based on multihit
        int numberOfHits = 1; // Hard-coded for now

        // TODO: calculate chance of each status chance based on damage instance types

        for (int i = 0; i < numberOfHits; i++) {
            // TODO: calculate whether the attack is critical
            // bool criticalHit = 

            // TODO: calculate number of statuses applied
            // int numberOfStatusProcs = 

            foreach (DamageInstance damageInstance in damageEvent.DamageInstances) {
                // Calculate damage based on critical
            }
        }
    }

    public void ProcessEvent(IEvent ev) {
        ProcessEvent((DamageEvent)ev);
    }
}