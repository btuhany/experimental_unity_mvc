using Zenject;
using UnityEngine;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.CounterText;
using Batuhan.MVC.Core;

namespace TimeCounter.Installers
{
    [CreateAssetMenu(fileName = "CounterEntityInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CounterEntityInstaller")]
    internal class CounterTextInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ICounterTextContext>().To<CounterTextContext>().AsTransient();
            container.Bind<ICounterTextModel>().To<CounterTextModel>().AsTransient();
            container.Bind<ILifeCycleHandler>().To<CounterTextController>().AsTransient();
        }
    }
}
