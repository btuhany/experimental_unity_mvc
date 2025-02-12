using Zenject;
using UnityEngine;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.CounterText;

namespace TimeCounter.Installers
{
    //TODOby: Dependency Injection base class?
    [CreateAssetMenu(fileName = "CounterEntityInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CounterEntityInstaller")]
    internal class CounterEntityInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ICounterTextContext>().To<CounterTextContext>().AsTransient();
            container.Bind<CounterTextModel>().AsTransient();
            container.Bind<CounterTextController>().AsTransient(); //TODOby Do we need to inject this?
            //container.Bind<IInitializable>().To<CounterController>().FromResolve();
            container.BindInterfacesTo<CounterTextController>().FromResolve();
        }
    }
}
