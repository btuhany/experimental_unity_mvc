using System;

namespace Assets.Scripts.Batuhan.EventBus
{
    public class EventBinding<T> where T : IEvent
    {
        public Action<T> OnEvent { get; private set; }
        public EventBinding(Action<T> onEvent) => OnEvent = onEvent;

        //public bool Equals(EventBinding<T> other)
        //{
        //    if (other is null)
        //        return false;
        //    return other.OnEvent == OnEvent;
        //}
        //public override bool Equals(object obj) => Equals(obj as EventBinding<T>);
        //public override int GetHashCode() => OnEvent?.GetHashCode() ?? 0;
    }
}
