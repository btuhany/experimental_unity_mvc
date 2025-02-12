using System;
using System.Collections.Generic;

namespace Batuhan.EventBus
{
    public interface IEventBindingCollection
    {
        bool IsEmpty { get; }
    }
    public class EventCollection<TEvent> : IEventBindingCollection where TEvent : IEvent
    {
        private readonly List<EventBinding<TEvent>> _bindings = new();
        private readonly Dictionary<Action<TEvent>, int> _bindingLookUpMap = new();
        public bool IsEmpty => _bindings.Count == 0;

        public void Add(EventBinding<TEvent> binding)
        {
            if (!_bindingLookUpMap.ContainsKey(binding.OnEvent))
            {
                _bindingLookUpMap[binding.OnEvent] = _bindings.Count;
                _bindings.Add(binding);
            }
        }

        public void Remove(Action<TEvent> callback)
        {
            if (_bindingLookUpMap.TryGetValue(callback, out int index))
            {
                // Swap-and-pop removal for O(1) performance
                int lastIndex = _bindings.Count - 1;
                _bindings[index] = _bindings[lastIndex];
                _bindingLookUpMap[_bindings[index].OnEvent] = index; // Update moved element index

                _bindings.RemoveAt(lastIndex);
                _bindingLookUpMap.Remove(callback);
            }
        }

        public void Invoke(TEvent eventData)
        {
            for (int i = 0; i < _bindings.Count; i++)
            {
                EventBinding<TEvent> binding = _bindings[i];
                try
                {
                    binding.OnEvent?.Invoke(eventData);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Exception in event handler: {ex}");
                }
            }
        }
    }
}
