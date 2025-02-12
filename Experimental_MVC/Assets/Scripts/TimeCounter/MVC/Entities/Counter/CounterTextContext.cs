using Batuhan.CommandManager;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.CounterText
{
    internal interface ICounterTextContext : IContext
    {
        public EventBus<TimeCounter.Events.Categories.Global> EventBusGlobal { get; }
        public CommandManager CommandManager { get; }
    }
    internal class CounterTextContext : ICounterTextContext
    {
        [Inject]
        public EventBus<TimeCounter.Events.Categories.Global> EventBusGlobal { get; }
        [Inject]
        public CommandManager CommandManager { get; }
    }
}
