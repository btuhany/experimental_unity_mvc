using Batuhan.EventBus;

namespace TimeCounter.Events
{
    namespace Categories
    {
        public class Global : IEventCategory
        {
            public EventCategoryID ID => EventCategoryID.Global;
        }
    }

    namespace Global
    {
        public struct AppInitializedEvent : IEvent
        {
            public EventCategoryID CategoryID => EventCategoryID.Global;
            public float Time;
        }
    }
}


