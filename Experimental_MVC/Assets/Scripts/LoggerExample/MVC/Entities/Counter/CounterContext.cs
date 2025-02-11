using Batuhan.CommandManager;
using Batuhan.Core.MVC;
using Batuhan.EventBus;
using Events.Category;
using Zenject;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal interface ICounterContext : IContext
    {
        public EventBus<Global> EventBusGlobal { get; }
        public CommandManager CommandManager { get; }
    }
    internal class CounterContext : ICounterContext
    {
        [Inject]
        public EventBus<Global> EventBusGlobal { get; }
        [Inject]
        public CommandManager CommandManager { get; }
    }
}
