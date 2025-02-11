using Batuhan.Core.MVC;
using Batuhan.EventBus;
using Events.Category;
using Zenject;

namespace Assets.Scripts.LoggerExample.MVC.Entities.AppInitializer
{
    internal interface IAppInitializerContext : IContext
    {
        public EventBus<Global> EventBusGlobal { get; }
    }

    internal class AppInitializerContext : IAppInitializerContext
    {
        [Inject]
        public EventBus<Global> EventBusGlobal { get; }
    }
}
