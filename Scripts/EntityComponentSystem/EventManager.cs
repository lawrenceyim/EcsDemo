using System.Collections.Generic;

namespace EntityComponentSystem;

public class EventManager {
    private List<IEvent> _events = [];

    public void AddEvent(IEvent e) {
        _events.Add(e);
    }

    public void ProcessEvents() {
        // In case an event generates a new event
        List<IEvent> events = _events;
        _events = [];
        foreach (IEvent e in events) {
            // TODO: process e
        }
    }
}