using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Batuhan.EventBus //TODOBY FIX NAMESPACES
{
    //Static implementations need bootstrapping to avoid allocations and potential performance spykes at runtime.
    //Using DI framework instead of a static class would be a better approach.
    public interface IEventCategory
    {
        public EventCategoryID ID { get; }
    }

    public interface IEventBus<TCategory>
        where TCategory : IEventCategory
    {
        //void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent;
        //void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent;
        //void Publish<TEvent>(TEvent eventData) where TEvent : IEvent;
    }
    public interface IEventBindingCollection
    {
        bool IsEmpty { get; }
    }


    public class EventBus<TCategory> : IEventBus<TCategory> where TCategory : IEventCategory, IDisposable 
    {
        private readonly Dictionary<Type, IEventBindingCollection> _bindings = new();
        private TCategory _category;
        public EventBus(TCategory category)
        {
            _category = category;
        }
        public void Dispose()
        {
            _bindings.Clear();
        }
        public void Publish<TEvent>(TEvent eventData) where TEvent : IEvent
        {
            if (!IsCategoryValid(eventData))
            {
                throw new Exception("Event Category is not valid for this event bus to publish!");
            }

            var eventType = typeof(TEvent);
            if (_bindings.TryGetValue(eventType, out var collection))
            {
                (collection as EventCollection<TEvent>)?.Invoke(eventData);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);

            if (!IsCategoryValid(eventType))
            {
                throw new Exception("Event Category is not valid for this event bus to subscribe!");
            }


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

            if (!IsCategoryValid(eventType))
            {
                throw new Exception("Event Category is not valid for this event bus to unsubscribe!");
            }

            if (_bindings.TryGetValue(eventType, out var collection))
            {
                var typedCollection = collection as EventCollection<TEvent>;
                typedCollection?.Remove(callback);

                if (typedCollection?.IsEmpty == true)
                {
                    _bindings.Remove(eventType);
                }
            }

        }

        private bool IsCategoryValid(IEvent eventData)
        {
            return _category.ID == GetCategoryID(eventData.GetType());
        }
        private bool IsCategoryValid(Type eventType)
        {
            return _category.ID == GetCategoryID(eventType);
        }

        //TODOBY not sure about this one's performance. Maybe a cache system at Initialize would be better.
        private EventCategoryID GetCategoryID(Type type)
        {
            if (typeof(IEvent).IsAssignableFrom(type))
            {
                return ((IEvent)Activator.CreateInstance(type)).CategoryID;
            }
            throw new Exception($"Type {type.Name} does not implement IEvent properly.");
        }
    }
}
