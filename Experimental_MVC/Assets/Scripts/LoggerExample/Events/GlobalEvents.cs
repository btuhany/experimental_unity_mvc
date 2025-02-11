using Batuhan.EventBus;

namespace Events.Category
{
    public class Global : IEventCategory
    {
        public EventCategoryID ID => EventCategoryID.Global;
    }

    public struct AppInitializedEvent : IEvent
    {
        public EventCategoryID CategoryID => EventCategoryID.Global;
        public float Time;
    }
}
