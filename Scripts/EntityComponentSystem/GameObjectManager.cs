using System.Collections.Generic;
using EntityComponentSystem;
using Godot;

public partial class GameObjectManager : Node, IAutoload {
    public static string AutoloadPath { get; } = "/root/GameObjectManager";
    public readonly Dictionary<ulong, Node> GameObjects = [];
    private static GameObjectManager _instance;

    // Do not add parameters. Autoload needs it
    private GameObjectManager() {
        _instance = this;
    }

    public static GameObjectManager GetInstance() {
        return _instance;
    }

    public void AddGameObject(ulong entityId, Node node) {
        AddChild(node);
        GameObjects[entityId] = node;
    }

    public void RemoveGameObject(ulong entityId) {
        Node node = GameObjects[entityId];
        node.QueueFree();
        GameObjects.Remove(entityId);
    }

    public Node GetGameObjectByEntityId(ulong entityId) {
        return GameObjects[entityId];
    }
}