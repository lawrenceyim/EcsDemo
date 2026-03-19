using System.Collections.Generic;

namespace EntityComponentSystem;

public class SystemManager {
    private Dictionary<ulong, ISystem> _systems = [];

    public void AddSystem(ulong id, ISystem system) {
        _systems[system.SystemId] = system;
    }

    public void RemoveSystem(ulong id) {
        _systems.Remove(id);
    }

    public ISystem GetSystem(ulong id) {
        return _systems[id];
    }
}