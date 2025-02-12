using Batuhan.CommandManager;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Installers.Mono;

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
            Container.Bind<CommandManager>().AsTransient();
        }
    }
}
