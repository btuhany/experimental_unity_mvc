using Batuhan.CommandManager;
using TimeCounter.Installers.Mono;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers.MonoBehaviours
{
    /// <summary>
    /// This class is used to install generic installers of the zenject.
    /// </summary>
    internal class MasterGenericInstallerHelper : MonoInstaller
    {
        public override void InstallBindings()
        {
            EventBusInstaller.Install(Container);
            Container.Bind<CommandManager>().AsTransient();
        }
    }
}
