using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Data;
using TimeCounter.Entities.Counter;
using UnityEngine;
using Zenject;

namespace TimeCounter.Installers
{
    [CreateAssetMenu(fileName = "TimeTickerInstaller", menuName = "Scriptable Objects/TimeCounterExample/Installers/TimeTickerInstaller")]
    internal class TickerInstaller : BaseEntityInstallerSO
    {
        [SerializeField] private TimeTickerModelDataSO _modelDataSO;
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ITickerContext>().To<TickerContext>().AsTransient();
            container.Bind<ITickerModel>().To<TickerModel>().AsSingle();
            container.Bind<ISceneLifeCycleManaged>().To<TickerController>().AsTransient();
            container.Bind<TimeTickerModelDataSO>().FromScriptableObject(_modelDataSO).AsTransient();

        }
    }
}
