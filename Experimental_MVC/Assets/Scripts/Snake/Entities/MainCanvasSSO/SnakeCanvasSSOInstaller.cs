using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using UnityEngine;
using Zenject;

namespace ExperimentalMVC.SnakeExample.Entities.MainCanvasSSO
{
    [CreateAssetMenu(fileName = "SnakeCanvasSSOInstaller", menuName = "Scriptable Objects/SnakeExample/Installers/SnakeCanvasSSOInstaller")]
    public class SnakeCanvasSSOInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ISceneLifeCycleManaged>().To<SnakeCanvasSSOController>().AsSingle();
        }
    }
}