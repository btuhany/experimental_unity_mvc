using System;

namespace Batuhan.CommandManager
{
    public interface ICommandBinding
    {
        Type CommandType { get; }
        void Execute(ICommand command);
        void Undo(ICommand command);

    }
    public class CommandBinding<TCommand> : ICommandBinding where TCommand : ICommand
    {
        public Type CommandType => typeof(TCommand);

        private readonly Action<TCommand> _execute;
        private readonly Action<TCommand> _undo;

        public CommandBinding(Action<TCommand> execute, Action<TCommand> undo = null)
        {
            _execute = execute;
            _undo = undo;
        }

        public void Execute(ICommand command)
        {
            if (command is TCommand typedCommand)
            {
                _execute?.Invoke(typedCommand);
                command.OnExecute();
            }
            else
            {
                throw new InvalidOperationException("Invalid command type");
            }
        }

        public void Undo(ICommand command)
        {
            if (command is TCommand typedCommand)
            {
                _undo?.Invoke(typedCommand);
                command.OnUndo();
            }
            else
            {
                throw new InvalidOperationException("Invalid command type");
            }
        }
        public bool ExecuteEquals(Action<TCommand> action) => _execute == action;
    }
}
