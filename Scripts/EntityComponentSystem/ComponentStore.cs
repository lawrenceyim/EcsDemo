using System;
using System.ComponentModel;

namespace EntityComponentSystem;

public class ComponentStore<T> where T : Component {
    private readonly SwapRemoveStorage<T> _swapRemoveStorage;

    public ComponentStore(int initialEntityCapacity, int initialComponentCapacity) {
        _swapRemoveStorage = new SwapRemoveStorage<T>(initialEntityCapacity, initialComponentCapacity);
    }

    public void Add(int entityId, T component) {
        _swapRemoveStorage.Add(entityId, component);
    }

    public void Remove(int entityId) {
        _swapRemoveStorage.Remove(entityId);
    }

    public ref T Get(int entityId) {
        return ref _swapRemoveStorage.Get(entityId);
    }

    public bool Has(int entityId) {
        return _swapRemoveStorage.Contains(entityId);
    }
}