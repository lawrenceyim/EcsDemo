using System.Collections.Generic;

namespace EntityComponentSystem;

public class EntityManager {
    public Dictionary<ulong, Entity> Entities;

    public void AddEntity(Entity entity) {
        Entities[entity.EntityId] = entity;
    }

    public void RemoveEntity(ulong entityId) {
        Entities.Remove(entityId);
    }

    public Entity GetEntity(ulong entityId) {
        return Entities[entityId];
    }
}