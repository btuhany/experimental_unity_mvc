using System;
using System.Collections.Generic;
using System.Linq;

namespace Batuhan.CommandManager
{
    //TODOBY: Composite command support
    public class CommandManager : ICommandManager
    {
        private readonly Dictionary<Type, List<ICommandBinding>> _commandListeners = new();
        private readonly Stack<ICommand> _executedCommands = new();

        public void AddListener<TCommand>(CommandBinding<TCommand> binding) where TCommand : ICommand
        {
            Type commandType = typeof(TCommand);
            if (!_commandListeners.ContainsKey(commandType))
            {
                _commandListeners[commandType] = new List<ICommandBinding>();
            }
            _commandListeners[commandType].Add(binding);

            SortListeners(commandType);
        }

        public void RemoveListener<TCommand>(CommandBinding<TCommand> binding) where TCommand : ICommand
        {
            Type commandType = typeof(TCommand);
            if (_commandListeners.ContainsKey(commandType))
            {
                _commandListeners[commandType].Remove(binding);
                if (_commandListeners[commandType].Count == 0)
                {
                    _commandListeners.Remove(commandType);
                }
            }
        }
        public void RemoveListenerFromExecuteCallback<TCommand>(Action<TCommand> executeCallback) where TCommand : ICommand
        {
            Type commandType = typeof(TCommand);
            if (_commandListeners.ContainsKey(commandType))
            {
                var listenersCopy = _commandListeners[commandType].ToArray();
                foreach (var binding in listenersCopy)
                {
                    if (binding is CommandBinding<TCommand> typedBinding && typedBinding.ExecuteEquals(executeCallback))
                    {
                        _commandListeners[commandType].Remove(binding);
                        if (_commandListeners[commandType].Count == 0)
                        {
                            _commandListeners.Remove(commandType);
                        }
                        break;
                    }
                }
            }
        }

        public void ExecuteCommand(ICommand command)
        {
            Type commandType = command.GetType();
            if (_commandListeners.ContainsKey(commandType))
            {
                foreach (var binding in _commandListeners[commandType])
                {
                    binding.Execute(command);
                }
            }
            _executedCommands.Push(command);
        }

        public void UndoCommand(ICommand command)
        {
            Type commandType = command.GetType();
            if (_commandListeners.ContainsKey(commandType))
            {
                foreach (var binding in _commandListeners[commandType])
                {
                    binding.Undo(command);
                }
            }
        }
        public void UndoLastExecutedCommand()
        {
            if (_executedCommands.Count > 0)
            {
                var lastCommand = _executedCommands.Pop();
                Type commandType = lastCommand.GetType();
                if (_commandListeners.ContainsKey(commandType))
                {
                    foreach (var binding in _commandListeners[commandType])
                    {
                        binding.Undo(lastCommand);
                    }
                }
            }
        }
        public void Dispose()
        {
            _commandListeners.Clear();
            _executedCommands.Clear();
        }
        private void SortListeners(Type type)
        {
            var commandList = _commandListeners[type];
            _commandListeners[type] = commandList.OrderByDescending(binding => binding.Priority).ToList();
        }
    }
}
