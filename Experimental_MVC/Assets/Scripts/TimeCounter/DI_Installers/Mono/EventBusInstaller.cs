using Batuhan.EventBus;
using TimeCounter.Events.CoreEvents;
using TimeCounter.Events.GlobalEvents;
using TimeCounter.Events.ModelEvents;
using UnityEngine.UIElements;
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

            Container.Bind<IEventBus<Global>>().To<EventBus<Global>>().AsSingle();
            Container.Bind<IEventBus<Core>>().To<EventBus<Core>>().AsSingle();
            Container.Bind<IEventBus<Model>>().To<EventBus<Model>>().AsTransient();
        }
    }
}
