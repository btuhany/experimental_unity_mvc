using Batuhan.EventBus;
using TimeCounter.Events.Categories;

namespace TimeCounter.Events.ModelEvents
{
    public class Model : IEventCategory
    {
        public int ID => EventCategory.Model.ToID();
    }

    public struct CountValueUpdatedEvent : IEvent
    {
        public int CategoryID => Categories.EventCategory.Model.ToID();
        public int NewValue;
    }
}