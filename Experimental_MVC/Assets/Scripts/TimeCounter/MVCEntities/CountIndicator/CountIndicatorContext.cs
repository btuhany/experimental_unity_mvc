using Batuhan.CommandManager;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.CountIndicator
{
    public interface ICountIndicatorContext : IContext
    {
        public IEventBus<Events.ModelEvents.Model> EventBusModel { get; }
        public ICommandManager CommandManager { get; }
    }
    internal class CountIndicatorContext : ICountIndicatorContext
    {
        [Inject]
        public IEventBus<Events.ModelEvents.Model> EventBusModel { get; }
        [Inject]
        public ICommandManager CommandManager { get; }
        public void Dispose()
        {
            EventBusModel.Dispose();
            CommandManager.Dispose();
        }
    }
}
