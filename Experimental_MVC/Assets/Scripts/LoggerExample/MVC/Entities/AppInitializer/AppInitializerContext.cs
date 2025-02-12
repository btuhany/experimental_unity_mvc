using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;
using UnityEditor.PackageManager;

namespace Assets.Scripts.LoggerExample.MVC.Entities.AppInitializer
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
