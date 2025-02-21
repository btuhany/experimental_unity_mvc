using Batuhan.MVC.UnityComponents.Zenject;
using UnityEngine;
namespace ExperimentalMVC.App
{
    [CreateAssetMenu(fileName = "ExperimentalMVCAppInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/ExperimentalMVCAppInstaller")]
    public class ExperimentalMVCAppInstallerSO : BaseAppReferenceManagerInstallerSO
    {
        public override void InstallBindings()
        {
            Container.Bind<IAppReferenceManager>().To<ExperimentalMVCReferenceManager>().AsSingle().NonLazy();
            Debug.Log("instal bindings");
        }
    }

}
