using Assets.Scripts.TimeCounter.Helper;
using Batuhan.CommandManager;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.CounterText
{
    public interface ICounterTextContext : IContext
    {
        public IEventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
        public IEventBus<Events.ModelEvents.Model> EventBusModel { get; }
        public IEventBus<Events.CoreEvents.Core> EventBusCore { get; }
        public ICommandManager CommandManager { get; }
        public IDebugHelper Debug { get; }

    }
    internal class CounterTextContext : ICounterTextContext
    {
        [Inject]
        public IEventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }        
        [Inject]
        public IEventBus<Events.ModelEvents.Model> EventBusModel { get; }
        [Inject]
        public IEventBus<Events.CoreEvents.Core> EventBusCore { get; }
        [Inject]
        public IDebugHelper Debug { get; }
        [Inject]
        public ICommandManager CommandManager { get; }
    }
}
