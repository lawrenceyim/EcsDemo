namespace EntityComponentSystem;

public interface ISystem {
    public ulong SystemId { get; }
    public void Update(double delta);
}