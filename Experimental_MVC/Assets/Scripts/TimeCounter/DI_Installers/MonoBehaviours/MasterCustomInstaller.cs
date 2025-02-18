using Assets.Scripts.TimeCounter.Helper;
using Batuhan.CommandManager;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.Initializer;

namespace TimeCounter.Installers
{
    internal class MasterCustomInstaller : BaseCustomInstallHelper
    {
        public override void InstallBindings()
        {
            EventBusInstaller.Install(Container);
            Container.Bind<SceneReferenceManager>().To<TimeCounterSceneReferenceManager>().AsSingle();
            Container.Bind<ICommandManager>().To<CommandManager>().AsTransient();
            Container.Bind<IDebugHelper>().To<DebugHelper>().AsSingle();
        }
    }
}
