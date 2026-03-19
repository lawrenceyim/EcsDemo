using System.Numerics;
using EntityComponentSystem;

public class PositionComponent : IComponent {
    public Vector2 Position { get; set; } = Vector2.Zero;
}