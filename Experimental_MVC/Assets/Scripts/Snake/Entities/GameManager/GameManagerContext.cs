using Batuhan.EventBus;
using Batuhan.MVC.Core;
using SnakeExample.Events;
using Zenject;

namespace SnakeExample.Entities.GameManager
{
    public interface IGameManagerContext : IContext
    {
        IEventBus<GameEvent> EventGameBus { get; }
    }
    public class GameManagerContext : IGameManagerContext
    {
        [Inject] public IEventBus<GameEvent> EventGameBus { get; }

        public void Dispose()
        {
        }
    }
}
