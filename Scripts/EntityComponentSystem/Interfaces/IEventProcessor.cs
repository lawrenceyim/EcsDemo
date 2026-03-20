namespace EntityComponentSystem;

public interface IEventProcessor {
    void ProcessEvent(IEvent ev);
}

public interface IEventProcessor<T> : IEventProcessor where T : IEvent {
    void ProcessEvent(T e);
}