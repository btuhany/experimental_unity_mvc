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
        [SerializeField] private TimeTickerModelDataSO _modelDataSO;
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ITimeTickerContext>().To<TimeTickerContext>().AsTransient();
            container.Bind<ITimeTickerModel>().To<TimeTickerModel>().AsTransient();
            container.Bind<ILifeCycleHandler>().To<TimeTickerController>().AsTransient();
            container.Bind<TimeTickerModelDataSO>().FromScriptableObject(_modelDataSO).AsTransient();

        }
    }
}
