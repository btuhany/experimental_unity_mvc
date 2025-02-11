using Assets.Scripts.LoggerExample.Installers.Mono;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
