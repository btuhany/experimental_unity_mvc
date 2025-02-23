using UnityEngine;
using Zenject;
namespace Batuhan.MVC.UnityComponents.Zenject
{
    /// <summary>
    /// Install this at project context.
    /// Note that AppReferenceManager remains in memory until the application is closed (if it binded AsSingle())
    /// </summary>
    [CreateAssetMenu(fileName = "CoreAppInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CoreAppInstaller")]
    public class CoreAppInstallerSO : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAppReferenceManager>().To<AppReferenceManager>().AsSingle();
        }
    }

}
