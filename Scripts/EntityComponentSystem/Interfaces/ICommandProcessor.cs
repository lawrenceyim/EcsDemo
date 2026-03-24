namespace EntityComponentSystem;

public interface ICommandProcessor<T> : ICommandProcessor where T : ICommand {
    public void Process(T command);

    void ICommandProcessor.Process(ICommand command) {
        Process((T)command);
    }
}

public interface ICommandProcessor {
    public void Process(ICommand command);
}