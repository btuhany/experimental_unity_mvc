
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using SnakeExample.Events;
using SnakeExample.Input;
using Zenject;

namespace SnakeExample.Entities.Snake
{
    internal class SnakeContext : IContext
    {
        [Inject] public ISnakeActionEventSource InputEventSource { get; }
        [Inject] public IEventBus<GameEvent> EventBus { get; }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
