using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using SnakeExample.Entities.ObstacleManager;
using UnityEngine;
using Zenject;

namespace SnakeExample.Installers
{
    [CreateAssetMenu(fileName = "ObstacleManagerInstaller", menuName = "Scriptable Objects/SnakeExample/Installers/ObstacleManagerInstaller")]
    internal class ObstacleManagerInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ISceneLifeCycleManaged>().To<ObstacleManagerController>().AsSingle();
            container.Bind<ObstacleManagerContext>().AsTransient();
        }
    }
}
