using Assets.Scripts.LoggerExample.MVC.Entities.AppInitializer;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AppInitializerInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/AppInitializerInstaller")]
    internal class AppInitializerInstaller : BaseEntityInstallerScriptableObject
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
