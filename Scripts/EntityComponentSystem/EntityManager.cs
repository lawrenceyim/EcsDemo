using System.Collections.Generic;
using Godot;

namespace EntityComponentSystem;

public class EntityManager {
    private readonly Dictionary<ulong, Entity> _entities = [];
    private ulong _nextEntityId = 0;

    public Entity CreateNewEntity() {
        Entity entity = new Entity(_nextEntityId++);
        return entity;
    }

    public void AddEntity(Entity entity) {
        _entities[entity.EntityId] = entity;
    }

    public void RemoveEntity(ulong entityId) {
        _entities.Remove(entityId);
    }

    public Entity GetEntity(ulong entityId) {
        return _entities[entityId];
    }
}