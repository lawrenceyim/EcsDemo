using EntityComponentSystem;
using Godot;

public class GameLoop : IAutoload, ITick {
    public static string AutoloadPath { get; } = "/root/GameClock";

    public static GameLoop _instance;

    public EntityManager EntityManager { get; private set; }
    public SystemManager SystemManager { get; private set; }
    public EventManager EventManager { get; private set; }
    public GameObjectManager GameObjectManager { get; private set; }
    private GameClock _gameClock;


    private Entity _testEntity;

    private GameLoop() {
        EntityManager = new EntityManager();
        SystemManager = new SystemManager(this);
        EventManager = new EventManager(this);
        GameObjectManager = GameObjectManager.GetInstance();
        _gameClock = GameClock.GetInstance();
        _gameClock.AddActiveScene(this, 0);
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
        EventManager.ProcessEvents();
        SystemManager.GetSystem<MovementSystem>().Update(delta);

        // TODO: remove this and refactor
        // (GameObjectManager.GetGameObjectByEntityId(_testEntity.EntityId) as Sprite2D).Position = EntityManager.GetEntity(0).GetComponent<PositionComponent>().Position;
    }

    public void PhysicsTick() {
        Update(1);
    }
}