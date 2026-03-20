using EntityComponentSystem;
using Godot;

public class SpawnPlayerCharacterEvent : IEvent {
    // DTO for character data to spawn
    public ulong PlayerId { get; set; }
    public Vector2 Position { get; set; }
}