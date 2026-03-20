namespace EntityComponentSystem;

public interface IEventProcessor {
    void ProcessEvent(IEvent ev);
}

public interface IEventProcessor<in T> : IEventProcessor where T : IEvent {
    void IEventProcessor.ProcessEvent(IEvent ev) {
        ProcessEvent(ev);
    }
}