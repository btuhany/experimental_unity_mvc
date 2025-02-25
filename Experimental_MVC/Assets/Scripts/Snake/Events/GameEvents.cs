using Batuhan.EventBus;
using SnakeExample.Entities.NewEntity;

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
        public GameManagerModel.GameState NewState;
    }
}
