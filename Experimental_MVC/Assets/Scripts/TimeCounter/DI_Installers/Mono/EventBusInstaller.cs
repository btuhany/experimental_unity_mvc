using Batuhan.EventBus;
using TimeCounter.Events.CoreEvents;
using TimeCounter.Events.GlobalEvents;
using TimeCounter.Events.ModelEvents;
using Zenject;

namespace TimeCounter.Installers
{ 
    internal class EventBusInstaller : Installer<EventBusInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Global>().AsSingle();
            Container.Bind<Model>().AsSingle();
            Container.Bind<Core>().AsSingle();

            Container.Bind<EventBus<Global>>().AsSingle();
            Container.Bind<EventBus<Core>>().AsSingle();

            Container.Bind<EventBus<Model>>().AsTransient();
        }
    }
}
