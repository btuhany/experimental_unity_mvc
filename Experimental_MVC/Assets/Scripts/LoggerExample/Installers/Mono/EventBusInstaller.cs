using Batuhan.EventBus;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers.Mono
{
    internal class EventBusInstaller : Installer<EventBusInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Events.Category.Global>().AsSingle();
            Container.Bind<EventBus<Events.Category.Global>>().AsSingle();
        }
    }
}
