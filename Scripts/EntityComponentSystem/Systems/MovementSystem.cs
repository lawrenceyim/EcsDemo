using System.Collections.Generic;
using EntityComponentSystem;
using Godot;

public class MovementSystem : ISystem {
    private Dictionary<ulong, Entity> _entities = [];
    private GameLoop _gameLoop;

    public MovementSystem(GameLoop gameLoop) {
        _gameLoop = gameLoop;
    }

    public void Update(double delta) {
        foreach (Entity entity in _entities.Values) {
            PositionComponent positionComponent = entity.GetComponent<PositionComponent>();
            positionComponent.Position += entity.GetComponent<MovementComponent>().Velocity;
            entity.UpdateComponent(typeof(PositionComponent), positionComponent);
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