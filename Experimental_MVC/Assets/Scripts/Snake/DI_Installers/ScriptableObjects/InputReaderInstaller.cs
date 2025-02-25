using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using SnakeExample.Entities.InputReader;
using UnityEngine;
using Zenject;

namespace SnakeExample.Installers
{
    [CreateAssetMenu(fileName = "InputReaderInstaller", menuName = "Scriptable Objects/SnakeExample/Installers/InputReaderInstaller")]
    public class InputReaderInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<IInputReaderContext>().To<InputReaderContext>().AsSingle();
            container.Bind<ISceneLifeCycleManaged>().To<InputReaderController>().AsSingle();
        }
    }
}
