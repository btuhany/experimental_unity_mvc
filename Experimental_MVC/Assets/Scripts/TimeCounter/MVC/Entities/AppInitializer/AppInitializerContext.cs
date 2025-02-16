using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.AppInitializer
{
    internal interface IAppInitializerContext : IContext
    {
        public EventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
    }

    internal class AppInitializerContext : IAppInitializerContext
    {
        [Inject]
        public EventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
    }
}
