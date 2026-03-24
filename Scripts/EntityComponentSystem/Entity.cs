public struct Entity {
    public int Id { get; }
    public ulong[] Mask { get; }

    public Entity(int id) {
        Id = id;
        Mask = new ulong[4];
    }

    public void AddComponent(int componentId) {
        MaskUtils.Set(Mask, componentId);
    }

    public void RemoveComponent(int componentId) {
        MaskUtils.Clear(Mask, componentId);
    }

    public bool HasComponent(int componentId) {
        return MaskUtils.Has(Mask, componentId);
    }
}