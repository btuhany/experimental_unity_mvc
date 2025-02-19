using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Data;
using TimeCounter.Entities.Counter;
using UnityEngine;
using Zenject;

namespace TimeCounter.Installers
{
    [CreateAssetMenu(fileName = "TimeTickerInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/TimeTickerInstaller")]
    internal class TimeTickerInstaller : BaseEntityInstallerSO
    {
        [SerializeField] private CounterModelDataSO _modelDataSO;
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ICounterContext>().To<CounterContext>().AsTransient();
            container.Bind<ICounterModel>().To<CounterModel>().AsTransient();
            container.Bind<ILifeCycleHandler>().To<CounterController>().AsTransient();
            container.Bind<CounterModelDataSO>().FromScriptableObject(_modelDataSO).AsTransient();
        }
    }
}
