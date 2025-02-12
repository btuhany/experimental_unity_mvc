using Batuhan.EventBus;
using TimeCounter.Events.Categories;

namespace TimeCounter.Events.Model
{
    public class Model : IEventCategory
    {
        public int ID => EventCategory.Global.ToID();
    }

    public struct TimeCountChangedEvent : IEvent
    {
        public int CategoryID => Categories.EventCategory.Model.ToID();
        public float Value;
    }
}