using System;
using TimeCounter.Events.ModelEvents;
namespace TimeCounter.Events
{
    namespace Categories
    {
        public enum EventCategory
        {
            Global = 0,
            Model = 1,
            CoreEvents = 2
        }
        public static class Utility
        {
            public static int ToID(this EventCategory categoryID)
            {
                return (int)categoryID;
            }
            public static System.Type GetType(this EventCategory categoryID)
            {
                return categoryID switch
                {
                    EventCategory.Global => typeof(GlobalEvents.Global),
                    EventCategory.Model => typeof(ModelEvents.Model),
                    _ => throw new ArgumentOutOfRangeException(nameof(categoryID), categoryID, null)
                };
            }
        }
    }
}
