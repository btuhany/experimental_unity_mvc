using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using SnakeExample.Events;
using SnakeExample.Grid;

namespace SnakeExample.Installers
{
    internal class MasterCustomInstallerSnake : BaseCustomInstallHelper
    {
        public override void InstallBindings()
        {
            Container.Bind<GameEvent>().AsSingle();
            Container.Bind<IEventBus<GameEvent>>().To<EventBus<GameEvent>>().AsSingle();

            Container.Bind<GridManager>().AsSingle();
            Container.Bind<ISceneLifeCycleManaged>().To<GridManager>().FromResolve();
            Container.Bind<IGridViewHelper>().To<GridManager>().FromResolve();

        }
    }
}
