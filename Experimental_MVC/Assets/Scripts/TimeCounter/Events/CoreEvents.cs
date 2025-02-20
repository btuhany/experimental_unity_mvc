using Batuhan.EventBus;
using TimeCounter.Events.Categories;

namespace TimeCounter.Events.CoreEvents
{
    public class Core : IEventCategory
    {
        public int ID => EventCategory.CoreEvents.ToID();

        public bool CanBeDisposed => false;
    }
    public struct TickCountValueUpdatedEvent : IValueUpdatedEvent<int>
    {
        public int CategoryID => Categories.EventCategory.CoreEvents.ToID();
        public int UpdatedValue { get; set; }
        public TickCountValueUpdatedEvent(int updatedValue)
        {
            UpdatedValue = updatedValue;
        }
    }
    public struct TickSpeedUpdatedEvent : IValueUpdatedEvent<float>
    {
        public float UpdatedValue { get; set; }

        public int CategoryID => Categories.EventCategory.CoreEvents.ToID();

        public TickSpeedUpdatedEvent(int updatedValue)
        {
            UpdatedValue = updatedValue;
        }
    }
    public interface IValueUpdatedEvent<T> : IEvent
    {
        public T UpdatedValue { get; set; }
    }
}
