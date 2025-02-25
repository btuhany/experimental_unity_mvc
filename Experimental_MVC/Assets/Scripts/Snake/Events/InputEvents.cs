using Batuhan.EventBus;
using SnakeExample.Events;

namespace SnakeExample.Assets.Scripts.Snake.Events
{
    public class Input : IEventCategory
    {
        public int ID => (int) EventCategory.Input;

        public bool CanBeDisposed => true;
    }

}
