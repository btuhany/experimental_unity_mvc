using Batuhan.EventBus;
using TimeCounter.Events.Categories;

namespace TimeCounter.Events.CoreEvents
{
    public class Core : IEventCategory
    {
        public int ID => EventCategory.CoreEvents.ToID();
    }
    public struct TimeCountValueUpdatedEvent : IEvent
    {
        public int CategoryID => Categories.EventCategory.CoreEvents.ToID();
        public int UpdatedValue { get; set; }
        public TimeCountValueUpdatedEvent(int updatedValue)
        {
            UpdatedValue = updatedValue;
        }
    }
}
