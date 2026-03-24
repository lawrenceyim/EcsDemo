using System.Collections.Generic;

namespace EntityComponentSystem;

public class CommandBuffer {
    private List<ICommand> _commands = [];
    private Dictionary<int, ICommandProcessor> _commandProcessors = [];
    private EntityManager _entityManager;

    public CommandBuffer(EntityManager entityManager) {
        _entityManager = entityManager;
        AddCommandProcessor(CommandId.GiveEntityItemCommand, new GiveEntityItemCommandProcessor(entityManager));
    }

    public void ProcessCommands() {
        foreach (ICommand command in _commands) {
            _commandProcessors[command.Id].Process(command);
        }
    }

    public void AddCommandProcessor(int commandId, ICommandProcessor commandProcessor) {
        _commandProcessors[commandId] = commandProcessor;
    }
}