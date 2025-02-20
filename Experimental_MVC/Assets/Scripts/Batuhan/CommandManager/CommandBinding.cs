using System;

namespace Batuhan.CommandManager
{
    public interface ICommandBinding
    {
        Type CommandType { get; }
        public int Priority { get; }
        void Execute(ICommand command);
        void Undo(ICommand command);

    }
    public class CommandBinding<TCommand> : ICommandBinding where TCommand : ICommand
    {
        public Type CommandType => typeof(TCommand);

        public int Priority => _priority;

        private readonly Action<TCommand> _execute;
        private readonly Action<TCommand> _undo;
        private int _priority;

        public CommandBinding(Action<TCommand> execute, Action<TCommand> undo = null, int priority = 0)
        {
            _execute = execute;
            _undo = undo;
            _priority = priority;
        }

        public void Execute(ICommand command)
        {
            if (command is TCommand typedCommand)
            {
                _execute?.Invoke(typedCommand);
                command.OnBindingExecute();
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
                command.OnBindingUndo();
            }
            else
            {
                throw new InvalidOperationException("Invalid command type");
            }
        }
        public bool ExecuteEquals(Action<TCommand> action) => (_execute == action);
    }
}
