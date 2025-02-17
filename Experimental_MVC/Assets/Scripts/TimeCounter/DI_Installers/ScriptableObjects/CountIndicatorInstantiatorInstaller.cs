using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.CounterText;
using TimeCounter.Entities.CountIndicatorManager;
using UnityEngine;
using Zenject;

namespace TimeCounter.Installers
{
    [CreateAssetMenu(fileName = "CountIndicatorInstantiatorInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CountIndicatorInstantiatorInstaller")]
    internal class CountIndicatorInstantiatorInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ICountIndicatorInstantiatorContext>().To<CountIndicatorInstantiatorContext>().AsTransient();
            container.Bind<ILifeCycleHandler>().To<CountIndicatorInstantiatorController>().AsTransient();
        }
    }
}
