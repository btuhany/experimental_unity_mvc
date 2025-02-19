using Assets.Scripts.TimeCounter.Helper;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.Counter
{
    public interface ITickerContext : IContext
    {
        public IEventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
        public IEventBus<Events.CoreEvents.Core> EventBusCore { get; }
        public IDebugHelper Debug { get; }
    }
    internal class TickerContext : ITickerContext
    {
        [Inject]
        public IEventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
        [Inject]
        public IEventBus<Events.CoreEvents.Core> EventBusCore { get; }
        [Inject]
        public IDebugHelper Debug { get; }

    }
}
