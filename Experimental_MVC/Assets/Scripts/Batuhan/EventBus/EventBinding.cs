using System;

namespace Batuhan.EventBus
{
    public class EventBinding<T> where T : IEvent 
    {
        public Action<T> OnEvent { get; private set; }
        public EventBinding(Action<T> onEvent) => OnEvent = onEvent;
    }
}
