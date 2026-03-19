using EntityComponentSystem;

public class GameLoop {
    public static GameLoop _instance;

    public EntityManager EntityManager { get; } = new();
    public SystemManager SystemManager { get; } = new();

    private GameLoop() { }

    public static GameLoop GetInstance() {
        _instance ??= new GameLoop();
        return _instance;
    }


    public void Update(double delta) { }
}