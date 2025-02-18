using Assets.Scripts.TimeCounter.Helper;
using Batuhan.CommandManager;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.CounterText
{
    public interface ICounterTextContext : IContext
    {
        public EventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
        public EventBus<Events.ModelEvents.Model> EventBusModel { get; }
        public EventBus<Events.CoreEvents.Core> EventBusCore { get; }
        public CommandManager CommandManager { get; }
        public DebugHelper Debug { get; }

    }
    internal class CounterTextContext : ICounterTextContext
    {
        [Inject]
        public EventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }        
        [Inject]
        public EventBus<Events.ModelEvents.Model> EventBusModel { get; }
        [Inject]
        public EventBus<Events.CoreEvents.Core> EventBusCore { get; }
        [Inject]
        public DebugHelper Debug { get; }
        [Inject]
        public CommandManager CommandManager { get; }
    }
}
