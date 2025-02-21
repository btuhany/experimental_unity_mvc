using Batuhan.EventBus;
using Batuhan.MVC.Core;
using TimeCounter.Entities.Counter;
using TimeCounter.Events.CoreEvents;
using Zenject;

namespace TimeCounter.Entities.InputHandler
{
    public interface IInputHandlerContext : IContext
    {
        ITickerModel TickerModel { get; }
        IEventBus<Events.CoreEvents.Core> EventBusCore{ get; }
    }
    public class InputHandlerContext : IInputHandlerContext
    {
        [Inject]
        public ITickerModel TickerModel { get; }

        [Inject]
        public IEventBus<Core> EventBusCore { get; }

        public void Dispose()
        {
        }
    }
}
    
