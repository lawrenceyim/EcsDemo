using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Godot;

namespace EntityComponentSystem;

public class EntityManager {
    private const int InitialSparseSetCapacity = 1_000_000;
    private const int InitialDenseSetCapacity = 10_000;
    private readonly Dictionary<int, Entity> _entities = [];
    private readonly Dictionary<int, IComponentStore> _componentStores = [];
    private readonly Dictionary<Archetype, List<int>> _archetypes = [];

    public List<int> GetEntitiesByComponentId(int componentId) {
        List<int> result = [];
        foreach (KeyValuePair<Archetype, List<int>> kvp in _archetypes) {
            if (MaskUtils.Has(kvp.Key.Mask, componentId)) {
                result.AddRange(kvp.Value);
            }
        }

        return result;
    }

    public List<int> GetEntitiesByMask(ulong[] mask) {
        List<int> result = [];
        foreach (KeyValuePair<Archetype, List<int>> kvp in _archetypes) {
            if (MaskUtils.HasAll(kvp.Key.Mask, mask)) {
                result.AddRange(kvp.Value);
            }
        }

        return result;
    }

    public void AddComponentStore<T>(int componentId) where T : IComponent {
        if (!_componentStores.TryAdd(componentId, new ComponentStore<T>(InitialSparseSetCapacity, InitialDenseSetCapacity))) {
            GD.PrintErr($"EntityManager could not add ComponentStore {componentId}");
        }
    }

    public ComponentStore<T> GetComponentStore<T>(int componentId) where T : IComponent {
        return (ComponentStore<T>)_componentStores[componentId];
    }

    public void AddEntityToArchetype(Archetype archetype, int entityId) {
        if (!_archetypes.TryGetValue(archetype, out List<int> group)) {
            group = [];
            _archetypes[archetype] = group;
        }

        group.Add(entityId);
    }

    public void RemoveEntityFromArchetype(Archetype archetype, int entityId) {
        if (_archetypes.TryGetValue(archetype, out List<int> group)) {
            group.Remove(entityId);
        }
    }

    public void AddEntity(Archetype archetype, int entityId) {
        AddEntityToArchetype(archetype, entityId);
        // TODO: Add to component stores new component
    }

    public void RemoveEntity(Archetype archetype, int entityId) {
        RemoveEntityFromArchetype(archetype, entityId);

        // bitmask method for performance
        Entity entity = _entities[entityId];
        for (int block = 0; block < 4; block++) {
            ulong bits = entity.Mask[block];

            while (bits != 0) {
                int bit = System.Numerics.BitOperations.TrailingZeroCount(bits);
                int componentId = block * 64 + bit;
                _componentStores[componentId].Remove(entityId);
                bits &= bits - 1;
            }
        }

        // brute force approach for readability
        // foreach (IComponentStore store in _componentStores.Values) {
        //     store.Remove(entityId);
        // }

        _entities.Remove(entityId);
    }

    public void AddComponent<T>(int entityId, int componentId, T component) where T : IComponent {
        ((ComponentStore<T>)_componentStores[componentId]).Add(entityId, component);
        _entities[entityId].AddComponent(componentId);
    }

    public void RemoveComponent<T>(int entityId, int componentId) where T : IComponent {
        ((ComponentStore<T>)_componentStores[componentId]).Remove(entityId);
        _entities[entityId].RemoveComponent(componentId);
    }

    public bool HasComponent(int entityId, int componentId) {
        return _entities[entityId].HasComponent(componentId);
    }
}