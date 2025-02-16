using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.CountIndicatorManager
{
    public interface ICountIndicatorInstantiatorContext : IContext 
    {
        public EventBus<Events.CoreEvents.Core> EventBusCore { get; }
        public EventBus<Events.ModelEvents.Model> EventBusModel { get; }
    }
    public class CountIndicatorInstantiatorContext : ICountIndicatorInstantiatorContext
    {
        [Inject]
        public EventBus<Events.CoreEvents.Core> EventBusCore { get; }
        [Inject]
        public EventBus<Events.ModelEvents.Model> EventBusModel { get; }
    }
}
