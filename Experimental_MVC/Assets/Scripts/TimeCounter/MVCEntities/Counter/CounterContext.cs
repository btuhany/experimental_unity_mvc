using Assets.Scripts.TimeCounter.Helper;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entitites.Counter
{
    public interface ICounterContext : IContext
    {
        public IEventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
        public IEventBus<Events.CoreEvents.Core> EventBusCore { get; }
        public IDebugHelper Debug { get; }
    }
    internal class CounterContext : ICounterContext
    {
        [Inject]
        public IEventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
        [Inject]
        public IEventBus<Events.CoreEvents.Core> EventBusCore { get; }
        [Inject]
        public IDebugHelper Debug { get; }

    }
}
