using System.Collections.Generic;
using System.Numerics;
using Godot;

namespace EntityComponentSystem;

// Figure out how to add AND, OR, NOT to query
// Make query a separate system?
public class EntityManager {
    private const int InitialSparseSetCapacity = 1_000_000;
    private const int InitialDenseSetCapacity = 10_000;
    private readonly Dictionary<int, Entity> _entities = [];
    private readonly Dictionary<int, IComponentStore> _componentStores = [];
    private readonly Dictionary<Archetype, List<int>> _archetypeEntities = [];
    private readonly ISystem[] _systems;
    private readonly CommandBuffer _commandBuffer;

    public EntityManager() {
        _commandBuffer = new CommandBuffer(this);
        _systems = _InitializeSystems();
    }

    public List<int> GetEntitiesByComponentId(int componentId) {
        List<int> result = [];
        foreach (KeyValuePair<Archetype, List<int>> kvp in _archetypeEntities) {
            if (MaskUtils.Has(kvp.Key.Mask, componentId)) {
                result.AddRange(kvp.Value);
            }
        }

        return result;
    }

    public List<int> GetEntitiesByMask(ulong[] mask) {
        List<int> result = [];
        foreach (KeyValuePair<Archetype, List<int>> kvp in _archetypeEntities) {
            if (MaskUtils.HasAll(kvp.Key.Mask, mask)) {
                result.AddRange(kvp.Value);
            }
        }

        return result;
    }

    public void AddComponentStore<T>(int componentId) where T : struct, IComponent {
        if (!_componentStores.TryAdd(componentId, new ComponentStore<T>(InitialSparseSetCapacity, InitialDenseSetCapacity))) {
            GD.PrintErr($"EntityManager could not add ComponentStore {componentId}");
        }
    }

    public ComponentStore<T> GetComponentStore<T>(int componentId) where T : struct, IComponent {
        return (ComponentStore<T>)_componentStores[componentId];
    }

    public void AddEntityToArchetype(Archetype archetype, int entityId) {
        if (!_archetypeEntities.TryGetValue(archetype, out List<int> group)) {
            group = [];
            _archetypeEntities[archetype] = group;
        }

        group.Add(entityId);
    }

    public void RemoveEntityFromArchetype(Archetype archetype, int entityId) {
        if (_archetypeEntities.TryGetValue(archetype, out List<int> group)) {
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
                int bit = BitOperations.TrailingZeroCount(bits);
                int componentId = block * 64 + bit;
                _componentStores[componentId].RemoveEntity(entityId);
                bits &= bits - 1;
            }
        }

        // brute force approach for readability
        // foreach (IComponentStore store in _componentStores.Values) {
        //     store.Remove(entityId);
        // }

        _entities.Remove(entityId);
    }

    public void AddComponent<T>(int entityId, int componentId, T component) where T : struct, IComponent {
        ((ComponentStore<T>)_componentStores[componentId]).AddComponent(entityId, component);
        _entities[entityId].AddComponent(componentId);
    }

    public void RemoveComponent<T>(int entityId, int componentId) where T : struct, IComponent {
        ((ComponentStore<T>)_componentStores[componentId]).RemoveEntity(entityId);
        _entities[entityId].RemoveComponent(componentId);
    }

    public bool HasComponent(int entityId, int componentId) {
        return _entities[entityId].HasComponent(componentId);
    }

    public ref T GetComponent<T>(int entityId, int componentId) where T : struct, IComponent {
        ComponentStore<T> store = (ComponentStore<T>)_componentStores[componentId];
        return ref store.GetComponent(entityId);
    }

    private ISystem[] _InitializeSystems() {
        ISystem[] systems = new ISystem[10];
        // Read from JSON or something for modding support

        return systems;
    }
}