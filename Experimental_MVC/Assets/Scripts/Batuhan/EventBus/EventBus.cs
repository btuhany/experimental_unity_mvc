using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Batuhan.EventBus //TODOBY FIX NAMESPACES
{
    //Static implementations need bootstrapping to avoid allocations and potential performance spykes at runtime.
    //Using DI framework instead of a static class would be a better approach.
    public class EventCategory
    {

    }

    public interface IEventBus<TCategory> 
        where TCategory : EventCategory
    {
        //void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent;
        //void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent;
        //void Publish<TEvent>(TEvent eventData) where TEvent : IEvent;
    }
    public interface IEventBindingCollection
    {
        bool IsEmpty { get; }
    }


    public class EventBus<TCategory> : IEventBus<TCategory> where TCategory : EventCategory
    {
        private readonly Dictionary<Type, IEventBindingCollection> _bindings = new();

        public void Publish<TEvent>(TEvent eventData) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (_bindings.TryGetValue(eventType, out var collection))
            {
                (collection as EventCollection<TEvent>)?.Invoke(eventData);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);

            if (!_bindings.TryGetValue(eventType, out var collection))
            {
                collection = new EventCollection<TEvent>();
                _bindings[eventType] = collection;
            }

            (collection as EventCollection<TEvent>)?.Add(new EventBinding<TEvent>(callback));
        }

        public void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);

            if (_bindings.TryGetValue(eventType, out var collection))
            {
                var typedCollection = collection as EventCollection<TEvent>;
                typedCollection?.Remove(new EventBinding<TEvent>(callback));

                if (typedCollection?.IsEmpty == true)
                {
                    _bindings.Remove(eventType);
                }
            }

        }
    }
}
