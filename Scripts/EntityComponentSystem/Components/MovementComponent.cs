using EntityComponentSystem;
using Godot;

public class MovementComponent : IComponent {
    public Vector2 Velocity { get; set; } = Vector2.Zero;
}