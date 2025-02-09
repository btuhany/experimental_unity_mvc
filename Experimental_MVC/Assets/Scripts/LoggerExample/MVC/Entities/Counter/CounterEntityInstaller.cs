using Zenject;
using UnityEngine;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterEntityInstaller : Installer<CounterEntityInstaller> 
    {
        public override void InstallBindings()
        {
            Container.Bind<CounterContext>().AsTransient();
            Container.Bind<CounterModel>().AsTransient();
            Container.Bind<CounterController>().AsTransient();
            Container.Bind<CounterEntityInitializer>().AsTransient(); //TODOby
        }
    }
}
