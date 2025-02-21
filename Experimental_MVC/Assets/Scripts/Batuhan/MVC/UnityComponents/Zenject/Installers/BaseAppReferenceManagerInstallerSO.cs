using Zenject;
namespace Batuhan.MVC.UnityComponents.Zenject
{
    public abstract class BaseAppReferenceManagerInstallerSO : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<IAppReferenceManager>().To<AppReferenceManager>().AsSingle();
        }
    }

}
