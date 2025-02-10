using Zenject;
using UnityEngine;
using Assets.Scripts.LoggerExample.Installers.ScriptableObjects;
using Zenject.Asteroids;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    //TODOby: Dependency Injection base class?
    [CreateAssetMenu(fileName = "CounterEntityInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CounterEntityInstaller")]
    internal class CounterEntityInstaller : BaseEntityInstallerScriptableObject
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<CounterContext>().AsTransient();
            container.Bind<CounterModel>().AsTransient();
            container.Bind<CounterController>().AsTransient(); //TODOby Do we need to inject this?
            container.Bind<IInitializable>().To<CounterController>().FromResolve();
        }
    }
}
