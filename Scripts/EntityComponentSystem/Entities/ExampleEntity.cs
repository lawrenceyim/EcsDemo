using EntityComponentSystem;

public class ExampleEntity : Entity {
    public ExampleEntity(ulong entityId) : base(entityId) {
        AddComponent(typeof(PositionComponent), new PositionComponent());
        AddComponent(typeof(MovementComponent), new MovementComponent());
    }
}