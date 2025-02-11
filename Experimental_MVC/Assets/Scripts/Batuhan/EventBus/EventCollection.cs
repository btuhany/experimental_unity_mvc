using System;
using System.Collections.Generic;
using UnityEngine;

namespace Batuhan.EventBus
{
    public interface IEventBindingCollection
    {
        bool IsEmpty { get; }
    }
    public class EventCollection<TEvent> : IEventBindingCollection where TEvent : IEvent
    {
        private readonly LinkedList<EventBinding<TEvent>> _bindings = new();
        private readonly Dictionary<Action<TEvent>, LinkedListNode<EventBinding<TEvent>>> _bindingLookUpMap = new();
        public bool IsEmpty => _bindings.Count == 0;

        public void Add(EventBinding<TEvent> binding)
        {
            if (!_bindingLookUpMap.ContainsKey(binding.OnEvent))
            {
                var node = _bindings.AddLast(binding);
                _bindingLookUpMap[binding.OnEvent] = node;
            }
        }

        public void Remove(Action<TEvent> callback)
        {
            if (_bindingLookUpMap.TryGetValue(callback, out var bindingToRemove))
            {
                _bindings.Remove(bindingToRemove);
                _bindingLookUpMap.Remove(callback);
            }
        }

        public void Invoke(TEvent eventData)
        {
            foreach (var binding in _bindings)
            {
                try
                {
                    binding.OnEvent?.Invoke(eventData);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Exception in event handler: {ex}");  //TODOby Seperate Debug class - Unity Debugger
                }
            }
        }
    }
}
