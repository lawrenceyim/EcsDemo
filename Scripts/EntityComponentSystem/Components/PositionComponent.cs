using EntityComponentSystem;
using Godot;

public class PositionComponent : IComponent {
    public Vector2 Position { get; set; } = Vector2.Zero;
}