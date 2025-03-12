using Batuhan.EventBus;
using Batuhan.MVC.UnityComponents.Zenject;
using SnakeExample.Events;

namespace SnakeExample.Installers
{
    internal class MasterCustomInstallerSnake : BaseCustomInstallHelper
    {
        public override void InstallBindings()
        {
            Container.Bind<GameEvent>().AsSingle();
            Container.Bind<IEventBus<GameEvent>>().To<EventBus<GameEvent>>().AsSingle();
        }
    }
}
