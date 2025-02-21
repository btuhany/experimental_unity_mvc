
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.InputHandler;
using UnityEngine;
using Zenject;

namespace TimeCounter.Installers
{

    [CreateAssetMenu(fileName = "InputHandlerInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/InputHandlerInstaller")]
    internal class InputHandlerInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ILifeCycleHandler>().To<InputHandlerController>().AsSingle();
            container.Bind<IInputHandlerContext>().To<InputHandlerContext>().AsSingle();
        }
    }
}
