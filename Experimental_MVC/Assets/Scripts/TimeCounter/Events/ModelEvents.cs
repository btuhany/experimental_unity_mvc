using Batuhan.EventBus;
using TimeCounter.Data;
using TimeCounter.Events.Categories;

namespace TimeCounter.Events.ModelEvents
{
    public class Model : IEventCategory
    {
        public int ID => EventCategory.Model.ToID();
    }

    public struct CounterValueUpdatedEvent : IEvent
    {
        public int CategoryID => Categories.EventCategory.Model.ToID();
        public int UpdatedValue { get; set; }
        public CounterValueUpdatedEvent(int updatedValue)
        {
            UpdatedValue = updatedValue;
        }
    }
    public struct CountIndicatorDataUpdatedEvent : IEvent
    {
        public int CategoryID => Categories.EventCategory.Model.ToID();
        public CountIndicatorCommonData Data { get; set; }
        public CountIndicatorDataUpdatedEvent(CountIndicatorCommonData data)
        {
            Data = data;
        }
    }
}