using System;
using System.Collections.Generic;
using Godot;

namespace EntityComponentSystem;

public class EventManager {
    private List<IEvent> _events = [];
    private Dictionary<Type, IEventProcessor> _eventProcessors = [];
    private GameLoop _gameLoop;

    public EventManager(GameLoop gameLoop) {
        _gameLoop = gameLoop;
        AddEventProcessor<SpawnPlayerCharacterEvent>(new SpawnCharacterProcessor(gameLoop));
    }

    public void AddEvent(IEvent e) {
        GD.Print($"Event added {e.GetType().Name}");
        _events.Add(e);
    }

    public void AddEventProcessor<T>(IEventProcessor processor) where T : IEvent {
        _eventProcessors[typeof(T)] = processor;
    }

    public void ProcessEvents() {
        List<IEvent> events = _events;
        _events = [];

        foreach (IEvent ev in events) {
            Type type = ev.GetType();
            GD.Print($"Processing event {type.Name}");
            if (_eventProcessors.TryGetValue(type, out IEventProcessor processor)) {
                GD.Print($"processor is {processor.GetType().Name}");
                processor.ProcessEvent(ev);
            }
        }
    }
}