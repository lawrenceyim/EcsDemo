using System.Collections.Generic;
using EntityComponentSystem;
using Godot;

public class MovementSystem : ISystem {
    public ulong SystemId { get; } = 2;
    private Dictionary<ulong, Entity> _entities = [];

    public void Update(double delta) {
        foreach (Entity entity in _entities.Values) {
            entity.GetComponent<PositionComponent>().Position += entity.GetComponent<MovementComponent>().Velocity;
        }
    }

    public void AddEntity(Entity entity) {
        if (!entity.HasComponent(typeof(PositionComponent)) || !entity.HasComponent(typeof(MovementComponent))) {
            GD.PrintErr($"AddEntity: entity {entity.EntityId} has no position or movement component.");
            return;
        }

        _entities[entity.EntityId] = entity;
    }

    public void RemoveEntity(Entity entity) {
        _entities[entity.EntityId] = null;
    }
}