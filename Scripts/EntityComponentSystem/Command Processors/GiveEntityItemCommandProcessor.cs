using EntityComponentSystem;

public class GiveEntityItemCommandProcessor : ICommandProcessor<GiveEntityItemCommand> {
    EntityManager _entityManager;

    public GiveEntityItemCommandProcessor(EntityManager entityManager) {
        _entityManager = entityManager;
    }

    public void Process(GiveEntityItemCommand command) {
        
    }
}