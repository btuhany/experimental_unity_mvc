using Assets.Scripts.LoggerExample.MVC.Entities.AppInitializer;
using Batuhan.MVC.UnityComponents.Zenject;
using UnityEngine;
using Zenject;

namespace TimeCounter.Installers
{
    [CreateAssetMenu(fileName = "AppInitializerInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/AppInitializerInstaller")]
    internal class AppInitializerInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<AppInitializerController>().AsSingle();
            container.Bind<AppInitializerModel>().AsTransient();
            container.Bind<AppInitializerView>().AsTransient();
            container.Bind<IAppInitializerContext>().To<AppInitializerContext>().AsTransient();
        }
    }
}
