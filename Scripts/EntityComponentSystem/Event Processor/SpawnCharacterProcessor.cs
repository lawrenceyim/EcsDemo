using EntityComponentSystem;
using Godot;

public class SpawnCharacterProcessor : IEventProcessor<SpawnPlayerCharacterEvent> {
    private GameLoop _gameLoop;

    public SpawnCharacterProcessor(GameLoop gameLoop) {
        _gameLoop = gameLoop;
    }

    public void ProcessEvent(SpawnPlayerCharacterEvent e) {
        // TODO: Get packed scene
        // TODO: initialize packed scene and customize to character data
        // TODO: Position character

        // TODO: Remove this placeholder
        Entity testEntity = _gameLoop.EntityManager.CreateNewEntity();
        _gameLoop.EntityManager.AddEntity(testEntity);
        MovementComponent movementComponent = new();
        movementComponent.Velocity = new Vector2(1, 0);
        testEntity.AddComponent(typeof(MovementComponent), movementComponent);
        testEntity.AddComponent(typeof(PositionComponent), new PositionComponent());
        Sprite2D sprite = new Sprite2D();
        // Create some sort of Render component
        sprite.Texture = GD.Load<Texture2D>("res://Assets/Sprites/Placeholder/icon.svg");
        _gameLoop.GameObjectManager.AddGameObject(testEntity.EntityId, sprite);
        _gameLoop.SystemManager.GetSystem<MovementSystem>().AddEntity(testEntity);
    }
}