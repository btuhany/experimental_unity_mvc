using System;

namespace Batuhan.CommandManager
{
    public interface ICommandManager
    {
        void AddListener<TCommand>(CommandBinding<TCommand> binding) where TCommand : ICommand;
        void RemoveListener<TCommand>(CommandBinding<TCommand> binding) where TCommand : ICommand;
        void RemoveListenerFromExecuteCallback<TCommand>(Action<TCommand> executeCallback) where TCommand : ICommand;
        void ExecuteCommand(ICommand command);
        void UndoCommand(ICommand command);
        void UndoLastExecutedCommand();
    }
}
