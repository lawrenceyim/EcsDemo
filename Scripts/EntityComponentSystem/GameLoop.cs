public class GameLoop : IAutoload, ITick {
    public static string AutoloadPath { get; } = "/root/GameClock";

    public static GameLoop _instance;
    private GameClock _gameClock;

    private GameLoop() {
        // GameObjectManager needs to be instantiated first since it is a dependency

        _gameClock = GameClock.GetInstance();
        _gameClock.AddActiveScene(this, 0);
    }

    public static GameLoop GetInstance() {
        _instance ??= new GameLoop();
        return _instance;
    }

    public void CloseGameLoop() {
        _instance = null;
    }

    public void Update(double delta) { }

    public void PhysicsTick() {
        Update(1);
    }
}