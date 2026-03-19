using System.Numerics;
using EntityComponentSystem;

public class MovementComponent : IComponent {
    public Vector2 Velocity { get; set; } = Vector2.Zero;
}