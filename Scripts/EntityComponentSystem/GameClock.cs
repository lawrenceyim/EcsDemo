using System.Collections.Generic;
using Godot;

public partial class GameClock : Node, IAutoload {
    public static string AutoloadPath { get; } = "/root/GameClock";

    private static GameClock _instance;
    private readonly Dictionary<ulong, ITick> _activeScenes = new();
    private bool _paused = false;

    private GameClock() {
        _instance = this;
    }

    public static GameClock GetInstance() {
        return _instance;
    }

    public void AddActiveScene(ITick scene, ulong id) {
        _activeScenes.Add(id, scene);
    }

    public void RemoveActiveScene(ulong id) {
        _activeScenes.Remove(id);
    }

    public void RemoveAllActiveScenes() {
        _activeScenes.Clear();
    }

    public void SetPauseState(bool paused) {
        _paused = paused;
    }

    public bool IsPaused() {
        return _paused;
    }

    public override void _PhysicsProcess(double delta) {
        if (_paused) {
            return;
        }

        foreach (ITick scene in _activeScenes.Values) {
            scene?.PhysicsTick();
        }
    }
}