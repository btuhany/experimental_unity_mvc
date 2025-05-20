
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using SnakeExample.Entities.GameManager;
using SnakeExample.Entities.InputReader;
using UnityEngine;
using Zenject;

namespace SnakeExample.Installers
{
    [CreateAssetMenu(fileName = "GameManagerInstaller", menuName = "Scriptable Objects/SnakeExample/Installers/GameManagerInstaller")]
    internal class GameManagerInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<IGameManagerContext>().To<GameManagerContext>().AsSingle();
            container.Bind<IGameManagerModel>().To<GameManagerModel>().AsSingle();
            container.Bind<ISceneLifeCycleManaged>().To<GameManagerController>().AsSingle();
        }
    }
}
