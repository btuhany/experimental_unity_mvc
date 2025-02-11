using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Batuhan.EventBus
{
    public class EventCollection<TEvent> : IEventBindingCollection where TEvent : IEvent
    {
        private readonly HashSet<EventBinding<TEvent>> _bindings = new();

        public bool IsEmpty => _bindings.Count == 0;

        public void Add(EventBinding<TEvent> binding) => _bindings.Add(binding);

        public void Remove(EventBinding<TEvent> binding) => _bindings.Remove(binding);

        public void Invoke(TEvent eventData)
        {
            foreach (var binding in _bindings.ToArray())
            {
                binding.OnEvent?.Invoke(eventData);
            }
        }
    }
}
