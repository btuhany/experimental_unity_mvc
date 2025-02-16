using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public abstract class BaseCustomInstallHelper : MonoInstaller
    {
        public abstract override void InstallBindings();
    }
}
