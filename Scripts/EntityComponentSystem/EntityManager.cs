using System;
using System.Collections.Generic;
using System.ComponentModel;
using Godot;

namespace EntityComponentSystem;

public class EntityManager {
    public HashSet<int> Entities { get; } = [];
    private readonly Dictionary<Type, object> _componentStores = [];
    private readonly Dictionary<Archetype, List<int>> _entityGroups = [];

    public void AddComponentStore<T>(ComponentStore<T> store) where T : Component {
        Type type = typeof(T);
        if (!_componentStores.TryAdd(type, store)) {
            GD.PrintErr($"EntityManager could not add ComponentStore {type}");
        }
    }

    public ComponentStore<T> GetComponentStore<T>() where T : Component {
        return _componentStores.GetValueOrDefault(typeof(T), null) as ComponentStore<T>;
    }

    public void AddEntity(Archetype archetype, int entityId) {
        if (!_entityGroups.TryGetValue(archetype, out List<int> group)) {
            group = [];
            _entityGroups[archetype] = group;
        }

        group.Add(entityId);
    }

    public void RemoveEntity(Archetype archetype, int entityId) {
        if (_entityGroups.TryGetValue(archetype, out List<int> group)) {
            group.Remove(entityId);
        }
    }
}