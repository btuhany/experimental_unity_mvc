using Batuhan.EventBus;
using Zenject;

namespace TimeCounter.Installers
{ 
    internal class EventBusInstaller : Installer<EventBusInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Events.Categories.Global>().AsSingle();
            Container.Bind<EventBus<Events.Categories.Global>>().AsSingle();
        }
    }
}
