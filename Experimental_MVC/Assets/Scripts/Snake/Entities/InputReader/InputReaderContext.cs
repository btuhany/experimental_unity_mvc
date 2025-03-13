using Batuhan.EventBus;
using Batuhan.MVC.Core;
using SnakeExample.Events;
using Zenject;

namespace SnakeExample.Entities.InputReader
{
    public interface IInputReaderContext : IContext
    {
        IEventBus<GameEvent> EventBus { get; }
    }
    public class InputReaderContext : IInputReaderContext
    {
        [Inject] IEventBus<GameEvent> EventBus { get; }
        public void Dispose()
        {
        }
    }
}
