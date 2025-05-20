using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using SnakeExample.Entities.Snake;
using UnityEngine;
using Zenject;

namespace SnakeExample.Installers
{
    [CreateAssetMenu(fileName = "SnakeInstaller", menuName = "Scriptable Objects/SnakeExample/Installers/SnakeInstaller")]
    internal class SnakeInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ISceneLifeCycleManaged>().To<SnakeController>().AsSingle();
            container.Bind<SnakeModel>().AsTransient();
            container.Bind<SnakeContext>().AsTransient();
        }
    }
}
