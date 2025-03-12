using Batuhan.EventBus;
using SnakeExample.Entities.GameManager;

namespace SnakeExample.Events
{
    public class GameEvent : IEventCategory
    {
        public int ID => (int) EventCategory.Game;

        public bool CanBeDisposed => true;
    }
    public struct GameStateChanged : IEvent
    {
        public int CategoryID => (int) EventCategory.Game;
        public GameState NewState;
    }
}
