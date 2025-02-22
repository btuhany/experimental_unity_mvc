using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using ExperimentalMVC.App.Entities;
using UnityEngine;
using Zenject;

namespace ExperimentalMVC.App.Installers
{
    [CreateAssetMenu(fileName = "MainSSOCanvasInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/MainSSOCanvasInstaller")]
    public class MainSSOCanvasInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<IAppLifeCycleManaged>().To<MainSSOCanvasController>().AsSingle();            
        }
    }
}
