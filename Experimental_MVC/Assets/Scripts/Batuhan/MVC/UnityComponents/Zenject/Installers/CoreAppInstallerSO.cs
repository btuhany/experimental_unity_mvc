using UnityEngine;
using Zenject;
namespace Batuhan.MVC.UnityComponents.Zenject
{
    [CreateAssetMenu(fileName = "CoreAppInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CoreAppInstaller")]
    public class CoreAppInstallerSO : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAppReferenceManager>().To<AppReferenceManager>().AsSingle();
        }
    }

}
