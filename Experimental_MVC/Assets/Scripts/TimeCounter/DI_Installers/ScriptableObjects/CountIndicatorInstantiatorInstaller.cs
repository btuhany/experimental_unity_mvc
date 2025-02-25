using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.CountIndicatorInstantiator;
using UnityEngine;
using Zenject;

namespace TimeCounter.Installers
{
    [CreateAssetMenu(fileName = "CountIndicatorInstantiatorInstaller", menuName = "Scriptable Objects/TimeCounterExample/Installers/CountIndicatorInstantiatorInstaller")]
    internal class CountIndicatorInstantiatorInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ICountIndicatorInstantiatorContext>().To<CountIndicatorInstantiatorContext>().AsTransient();
            container.Bind<ICountIndicatorInstantiatorModel>().To<CountIndicatorInstantiatorModel>().AsTransient();
            container.Bind<ISceneLifeCycleManaged>().To<CountIndicatorInstantiatorController>().AsTransient();
        }
    }
}
