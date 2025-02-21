using Zenject;
namespace Batuhan.MVC.UnityComponents.Zenject
{
    public abstract class BaseAppReferenceManagerInstallerSO : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAppReferenceManager>().To<AppReferenceManager>().AsSingle();
        }
    }

}
