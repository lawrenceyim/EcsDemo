namespace EntityComponentSystem;

public class ComponentStore<T> : IComponentStore where T : struct, IComponent {
    private readonly SwapRemoveStorage<T> _swapRemoveStorage;

    public ComponentStore(int initialEntityCapacity, int initialComponentCapacity) {
        _swapRemoveStorage = new SwapRemoveStorage<T>(initialEntityCapacity, initialComponentCapacity);
    }

    public void AddComponent(int entityId, T component) {
        _swapRemoveStorage.Add(entityId, component);
    }

    public void RemoveEntity(int entityId) {
        _swapRemoveStorage.Remove(entityId);
    }

    public ref T GetComponent(int entityId) {
        return ref _swapRemoveStorage.Get(entityId);
    }

    public bool HasComponent(int entityId) {
        return _swapRemoveStorage.Contains(entityId);
    }
}