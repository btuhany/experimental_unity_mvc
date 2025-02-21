using Assets.Scripts.TimeCounter.Helper;
using Batuhan.CommandManager;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.CounterText
{
    public interface ICounterTextContext : IContext
    {
        public IEventBus<Events.CoreEvents.Core> EventBusCore { get; }
        public ICommandManager CommandManager { get; }
        public IDebugHelper Debug { get; }

    }
    internal class CounterTextContext : ICounterTextContext
    {
        [Inject]
        public IEventBus<Events.CoreEvents.Core> EventBusCore { get; }
        [Inject]
        public IDebugHelper Debug { get; }
        [Inject]
        public ICommandManager CommandManager { get; }

        public void Dispose()
        {
            Debug.Dispose();
            CommandManager.Dispose();
        }
    }
}
