using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.EntryPoint;
using UnityEngine;
using Zenject;

namespace TimeCounter.Installers
{
    [CreateAssetMenu(fileName = "EntryPointInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/EntryPointInstaller")]
    internal class EntryPointInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<EntryPointController>().AsSingle();
            container.Bind<ISceneLifeCycleManaged>().To<EntryPointController>().FromResolve();
            container.Bind<IEntryPoint>().To<EntryPointController>().FromResolve();

            container.Bind<IEntryPointContext>().To<EntryPointContext>().AsTransient();
        }
    }
}