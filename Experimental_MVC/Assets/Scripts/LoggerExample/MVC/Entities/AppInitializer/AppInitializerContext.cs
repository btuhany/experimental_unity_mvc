using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.AppInitializer
{
    internal interface IAppInitializerContext : IContext
    {
        public EventBus<TimeCounter.Events.Categories.Global> EventBusGlobal { get; }
    }

    internal class AppInitializerContext : IAppInitializerContext
    {
        [Inject]
        public EventBus<TimeCounter.Events.Categories.Global> EventBusGlobal { get; }
    }
}
