using Batuhan.CommandManager;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.CountIndicator
{
    internal interface ICountIndicatorContext : IContext
    {
        public EventBus<Events.ModelEvents.Model> EventBusModel { get; }
        public CommandManager CommandManager { get; }
    }
    internal class CountIndicatorContext : ICountIndicatorContext
    {
        [Inject]
        public EventBus<Events.ModelEvents.Model> EventBusModel { get; }
        [Inject]
        public CommandManager CommandManager { get; }
    }
}
