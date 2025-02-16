using Batuhan.EventBus;
using TimeCounter.Events.Categories;

namespace TimeCounter.Events.GlobalEvents
{
    public class Global : IEventCategory
    {
        public int ID => EventCategory.Global.ToID();
    }
    public struct AppInitializedEvent : IEvent
    {
        public int CategoryID => Categories.EventCategory.Global.ToID();
        public float Time;
    }
}


