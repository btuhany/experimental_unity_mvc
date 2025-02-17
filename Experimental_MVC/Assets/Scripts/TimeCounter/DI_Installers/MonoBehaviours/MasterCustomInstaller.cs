using Assets.Scripts.Batuhan.RuntimeCopyScriptableObjects;
using Assets.Scripts.TimeCounter.Entities.EntryPoint;
using Assets.Scripts.TimeCounter.Helper;
using Batuhan.CommandManager;
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.CounterText;
using TimeCounter.Entities.Initializer;

namespace TimeCounter.Installers
{
    internal class MasterCustomInstaller : BaseCustomInstallHelper
    {
        public override void InstallBindings()
        {
            EventBusInstaller.Install(Container);
            Container.Bind<RuntimeClonableSOManager>().AsSingle();
            Container.Bind<SceneReferenceManager>().To<TimeCounterSceneReferenceManager>().AsSingle();
            Container.Bind<CommandManager>().AsTransient();
            Container.Bind<DebugHelper>().AsSingle();
        }
    }
}
