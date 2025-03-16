using Batuhan.EventBus;
using Batuhan.MVC.Core;
using SnakeExample.Events;
using SnakeExample.Input;
using Zenject;

namespace SnakeExample.Entities.GameManager
{
    public interface IGameManagerContext : IContext
    {
        IEventBus<GameEvent> EventGameBus { get; }
        IGlobalInputActionEventSource InputSource { get; }
    }
    public class GameManagerContext : IGameManagerContext
    {
        [Inject] public IEventBus<GameEvent> EventGameBus { get; }
        [Inject] public IGlobalInputActionEventSource InputSource { get; }
        
        public void Dispose()
        {
        }
    }
}
