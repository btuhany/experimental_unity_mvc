using Zenject;
using UnityEngine;
using Assets.Scripts.LoggerExample.Installers.ScriptableObjects;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    [CreateAssetMenu(fileName = "CounterEntityInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CounterEntityInstaller")]
    internal class CounterEntityInstaller : BaseEntityInstallerScriptableObject
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<CounterContext>().AsTransient();
            container.Bind<CounterModel>().AsTransient();
            container.Bind<CounterController>().AsTransient();
            container.Bind<CounterEntityInitializer>().AsTransient(); //TODOby
        }
    }
}
