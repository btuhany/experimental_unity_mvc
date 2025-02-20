using System;
using System.Collections.Generic;
using System.Linq;

namespace Batuhan.CommandManager
{
    //TODOBY: Composite command support
    public class CommandManager : ICommandManager
    {
        private readonly Dictionary<Type, List<ICommandBinding>> _listenerMap = new();
        private readonly Stack<ICommand> _executedCommands = new();

        public void AddListener<TCommand>(CommandBinding<TCommand> binding) where TCommand : ICommand
        {
            Type commandType = typeof(TCommand);
            if (!_listenerMap.ContainsKey(commandType))
            {
                _listenerMap[commandType] = new List<ICommandBinding>();
            }
            _listenerMap[commandType].Add(binding);

            SortListeners(commandType);
        }

        public void RemoveListener<TCommand>(CommandBinding<TCommand> binding) where TCommand : ICommand
        {
            Type commandType = typeof(TCommand);
            if (_listenerMap.ContainsKey(commandType)) 
            {
                var commandList = _listenerMap[commandType];
                commandList.Remove(binding);
                if (commandList.Count == 0)
                {
                    _listenerMap.Remove(commandType);
                }
            }
        }
        public void RemoveListenerFromExecuteCallback<TCommand>(Action<TCommand> executeCallback) where TCommand : ICommand
        {
            Type commandType = typeof(TCommand);
            if (_listenerMap.ContainsKey(commandType))
            {
                var listenersCopy = _listenerMap[commandType].ToArray();
                foreach (var binding in listenersCopy)
                {
                    if (binding is CommandBinding<TCommand> typedBinding && typedBinding.ExecuteEquals(executeCallback))
                    {
                        _listenerMap[commandType].Remove(binding);
                        if (_listenerMap[commandType].Count == 0)
                        {
                            _listenerMap.Remove(commandType);
                        }
                        break;
                    }
                }
            }
        }

        public void ExecuteCommand(ICommand command)
        {
            Type commandType = command.GetType();
            if (_listenerMap.ContainsKey(commandType))
            {
                foreach (var binding in _listenerMap[commandType])
                {
                    binding.Execute(command);
                }
            }
            command.OnExecute();
            _executedCommands.Push(command);
        }

        public void UndoCommand(ICommand command)
        {
            Type commandType = command.GetType();
            if (_listenerMap.ContainsKey(commandType))
            {
                foreach (var binding in _listenerMap[commandType])
                {
                    binding.Undo(command);
                }
            }
            command.OnUndo();
        }
        public void UndoLastExecutedCommand()
        {
            if (_executedCommands.Count > 0)
            {
                var lastCommand = _executedCommands.Pop();
                Type commandType = lastCommand.GetType();
                if (_listenerMap.ContainsKey(commandType))
                {
                    foreach (var binding in _listenerMap[commandType])
                    {
                        binding.Undo(lastCommand);
                    }
                }
            }
        }
        public void Dispose()
        {
            _listenerMap.Clear();
            _executedCommands.Clear();
        }
        private void SortListeners(Type type)
        {
            var commandList = _listenerMap[type];
            _listenerMap[type] = commandList.OrderByDescending(binding => binding.Priority).ToList();
        }

        internal void AddListener<T>(object onUpdateCounterTextCommand)
        {
            throw new NotImplementedException();
        }
    }
}
