using Assets.Scripts.TimeCounter.Helper;
using Batuhan.CommandManager;
using Batuhan.MVC.UnityComponents.Zenject;

namespace TimeCounter.Installers
{
    /// <summary>
    /// This class is used to install generic installers of the zenject.
    /// </summary>
    internal class MasterCustomInstaller : BaseCustomInstallHelper
    {
        public override void InstallBindings()
        {
            EventBusInstaller.Install(Container);
            InitializerInstaller.Install(Container);
            Container.Bind<CommandManager>().AsTransient();
            Container.Bind<DebugHelper>().AsSingle();
        }
    }
}
