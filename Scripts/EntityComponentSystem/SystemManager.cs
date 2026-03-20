using System;
using System.Collections.Generic;
using Godot;

namespace EntityComponentSystem;

public class SystemManager {
    private readonly Dictionary<Type, ISystem> _systems = [];
    private GameLoop _gameLoop;

    public SystemManager(GameLoop gameLoop) {
        _gameLoop = gameLoop;

        AddSystem<MovementSystem>(new MovementSystem(gameLoop));
        AddSystem<RenderSystem>(new RenderSystem(gameLoop));
    }

    public void AddSystem<T>(ISystem system) where T : ISystem {
        if (_systems.ContainsKey(typeof(T))) {
            GD.PushError($"System with ID {typeof(T)} already exists");
            return;
        }

        _systems[typeof(T)] = system;
    }

    public void RemoveSystem<T>() where T : ISystem {
        _systems.Remove(typeof(T));
    }

    public T GetSystem<T>() where T : ISystem {
        return (T)_systems[typeof(T)];
    }
}