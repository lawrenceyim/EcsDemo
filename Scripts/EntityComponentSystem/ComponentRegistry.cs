using System;
using System.Collections.Generic;

namespace EntityComponentSystem;

public class ComponentRegistry {
    private readonly Dictionary<ulong[], Type> _components = [];

    public void RegisterComponent(ulong[] mask, Type componentType) {
        _components[mask] = componentType;
    }

    public Type GetComponent(ulong[] mask) {
        return _components[mask];
    }
}