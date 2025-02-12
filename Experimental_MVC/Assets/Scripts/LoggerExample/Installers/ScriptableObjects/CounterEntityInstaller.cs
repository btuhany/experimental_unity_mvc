using Zenject;
using UnityEngine;
using Assets.Scripts.LoggerExample.Installers.ScriptableObjects;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    //TODOby: Dependency Injection base class?
    [CreateAssetMenu(fileName = "CounterEntityInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CounterEntityInstaller")]
    internal class CounterEntityInstaller : BaseEntityInstallerScriptableObject
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
