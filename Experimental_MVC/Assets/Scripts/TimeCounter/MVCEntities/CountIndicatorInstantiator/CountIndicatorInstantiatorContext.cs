using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.CountIndicatorInstantiator
{
    public interface ICountIndicatorInstantiatorContext : IContext 
    {
        public IEventBus<Events.CoreEvents.Core> EventBusCore { get; }
        public IEventBus<Events.ModelEvents.Model> EventBusModel { get; }
    }
    public class CountIndicatorInstantiatorContext : ICountIndicatorInstantiatorContext
    {
        [Inject]
        public IEventBus<Events.CoreEvents.Core> EventBusCore { get; }
        [Inject]
        public IEventBus<Events.ModelEvents.Model> EventBusModel { get; }

        public void Dispose()
        {
            EventBusModel.Dispose();
        }
    }
}
