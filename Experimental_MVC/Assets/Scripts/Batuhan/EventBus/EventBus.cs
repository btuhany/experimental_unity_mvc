using System;
using System.Collections.Generic;

namespace Batuhan.EventBus
{
    //Static implementations need bootstrapping to avoid allocations and potential performance spykes at runtime.
    //Using DI framework instead of a static class would be a better approach.
    public interface IEventBus : IDisposable
    {
        void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent;
        void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent;
        void Publish<TEvent>(TEvent eventData) where TEvent : IEvent;
        public void CleanUp();
    }
    public interface IEventBus<TCategory> : IEventBus
        where TCategory : IEventCategory
    {
        public TCategory Category { get; }
    }
    //TODOBY IEventCategory could be used with composite
    public class EventBus<TCategory> : IDisposable, IEventBus<TCategory> where TCategory : IEventCategory
    {
        private readonly Dictionary<Type, IEventBindingCollection> _bindings = new();
        private static readonly Dictionary<Type, int> _categoryCache = new(); //TODOBY use a different helper class for caching
        private TCategory _category;
        public TCategory Category => _category;
        public EventBus(TCategory category)
        {
            _category = category;
        }
        public void CleanUp()
        {
            List<Type> keysToRemove = new();
            foreach (var kvp in _bindings)
            {
                if (kvp.Value.IsEmpty)
                    keysToRemove.Add(kvp.Key);
            }

            foreach (var key in keysToRemove)
            {
                _bindings.Remove(key);
            }
        }
        public void Dispose()
        {
            if (_category.CanBeDisposed)
            {
                CleanUp();
                _bindings.Clear();
            }
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
                ((EventCollection<TEvent>) collection).Invoke(eventData);
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

            ((EventCollection<TEvent>) collection).Add(new EventBinding<TEvent>(callback));
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

        //Not sure about the performance of this one.
        private int GetCategoryID(Type type)
        {
            if (_categoryCache.TryGetValue(type, out var categoryID))
            {
                return categoryID;
            }

            var categoryProperty = type.GetProperty(nameof(IEvent.CategoryID));
            if (categoryProperty != null && categoryProperty.CanRead)
            {
                var instance = Activator.CreateInstance(type);
                categoryID = (int) categoryProperty.GetValue(instance);
                _categoryCache[type] = categoryID;
                return categoryID;
            }

            throw new InvalidOperationException($"Type {type.Name} does not implement IEvent properly.");
        }
    }
}
