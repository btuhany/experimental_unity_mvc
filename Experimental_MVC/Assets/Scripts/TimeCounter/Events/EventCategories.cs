using Batuhan.EventBus;
using System;
namespace TimeCounter.Events
{
    namespace Categories
    {
        public enum EventCategory
        {
            Global = 0,
            Model = 1
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
                    EventCategory.Global => typeof(Global),
                    EventCategory.Model => typeof(Model),
                    _ => throw new ArgumentOutOfRangeException(nameof(categoryID), categoryID, null)
                };
            }
        }
    }
}
