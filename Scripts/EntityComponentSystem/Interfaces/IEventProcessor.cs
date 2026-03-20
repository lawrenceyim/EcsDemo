namespace EntityComponentSystem;

public interface IEventProcessor {
    void ProcessEvent(IEvent ev);
}

public interface IEventProcessor<in T> : IEventProcessor where T : IEvent {
    void ProcessEvent(T ev);

    // Bridge: routes non-generic call to generic handler
    void IEventProcessor.ProcessEvent(IEvent ev) {
        // Safe cast required for dispatch
        ProcessEvent((T)ev);
    }
}