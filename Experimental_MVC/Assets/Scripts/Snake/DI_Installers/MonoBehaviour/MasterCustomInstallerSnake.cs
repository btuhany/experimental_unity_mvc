using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using SnakeExample.Events;
using SnakeExample.Grid;
using SnakeExample.Input;
using SnakeExample.Tick;

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
            Container.Bind<IGridModelHelper>().To<GridManager>().FromResolve();

            Container.Bind<TickManager>().AsSingle();
            Container.Bind<ISceneLifeCycleManaged>().To<TickManager>().FromResolve();
            Container.Bind<IEntryPoint>().To<TickManager>().FromResolve();

            Container.Bind<GameInputDispatcher>().AsSingle();
            Container.Bind<IGameInputDispatcher>().To<GameInputDispatcher>().FromResolve();
            Container.Bind<ISnakeActionEventSource>().To<GameInputDispatcher>().FromResolve();
            Container.Bind<IGlobalInputActionEventSource>().To<GameInputDispatcher>().FromResolve();

            Container.Bind<GameData>().AsSingle();
        }
    }
}
