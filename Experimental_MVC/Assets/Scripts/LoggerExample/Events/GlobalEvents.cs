using Batuhan.EventBus;

namespace TimeCounter.Events.Categories
{
    public class Global : IEventCategory
    {
        public EventCategoryID ID => EventCategoryID.Global;
    }
}

namespace TimeCounter.Events.Global
{
    public struct AppInitializedEvent : IEvent
    {
        public EventCategoryID CategoryID => EventCategoryID.Global;
        public float Time;
    }
}
