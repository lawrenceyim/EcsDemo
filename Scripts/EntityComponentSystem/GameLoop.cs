using EntityComponentSystem;
using Godot;

public class GameLoop : IAutoload, ITick {
    public static string AutoloadPath { get; } = "/root/GameClock";

    public static GameLoop _instance;

    public EntityManager EntityManager { get; private set; }
    public SystemManager SystemManager { get; private set; }
    private GameObjectManager _gameObjectManager;
    private GameClock _gameClock;


    private Entity _testEntity;

    private GameLoop() {
        EntityManager = new EntityManager();
        SystemManager = new SystemManager();
        _gameObjectManager = GameObjectManager.GetInstance();
        _gameClock = GameClock.GetInstance();
        _gameClock.SetPauseState(true);
        _gameClock.AddActiveScene(this, 0);

        // TODO: Remove test code
        MovementSystem movementSystem = new MovementSystem();
        SystemManager.AddSystem(movementSystem.SystemId, movementSystem);

        _testEntity = EntityManager.CreateNewEntity();
        GD.Print($"Test Entity is null {_testEntity is null}");
        EntityManager.AddEntity(_testEntity);
        MovementComponent movementComponent = new MovementComponent();
        movementComponent.Velocity = new Vector2(1, 0);
        _testEntity.AddComponent(typeof(MovementComponent), movementComponent);
        _testEntity.AddComponent(typeof(PositionComponent), new PositionComponent());
        Sprite2D sprite = new Sprite2D();
        // Create some sort of Render component
        sprite.Texture = GD.Load<Texture2D>("res://Assets/Sprites/Placeholder/icon.svg");
        _gameObjectManager.AddEntity(_testEntity.EntityId, sprite);
        movementSystem.AddEntity(_testEntity);
        _gameClock.SetPauseState(false);
    }

    public static GameLoop GetInstance() {
        _instance ??= new GameLoop();
        return _instance;
    }

    public void CloseGameLoop() {
        EntityManager = null;
        SystemManager = null;
        _instance = null;
    }

    public void Update(double delta) {
        // TODO: Change to actual system
        SystemManager.GetSystem(2).Update(delta);
        (_gameObjectManager.GetNodeByEntityId(_testEntity.EntityId) as Sprite2D).Position = EntityManager.GetEntity(0).GetComponent<PositionComponent>().Position;
    }

    public void PhysicsTick() {
        Update(1);
    }
}