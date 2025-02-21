using Batuhan.EventBus;
using TimeCounter.Events.Categories;

namespace TimeCounter.Events.GlobalEvents
{
    public class Global : IEventCategory
    {
        public int ID => EventCategory.Global.ToID();

        public bool CanBeDisposed => false;
    }
    public struct SceneInitializedEvent : IEvent
    {
        public int CategoryID => Categories.EventCategory.Global.ToID();
    }
}


