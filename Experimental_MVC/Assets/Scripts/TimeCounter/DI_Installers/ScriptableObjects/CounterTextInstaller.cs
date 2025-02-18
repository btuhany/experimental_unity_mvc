using Zenject;
using UnityEngine;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.CounterText;
using Batuhan.MVC.Core;
using TimeCounter.Data;

namespace TimeCounter.Installers
{
    [CreateAssetMenu(fileName = "CounterEntityInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CounterEntityInstaller")]
    internal class CounterTextInstaller : BaseEntityInstallerSO
    {
        [SerializeField] private CounterTextModelDataSO _modelDataSO;
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ICounterTextContext>().To<CounterTextContext>().AsTransient();
            container.Bind<ICounterTextModel>().To<CounterTextModel>().AsTransient();
            container.Bind<ILifeCycleHandler>().To<CounterTextController>().AsTransient();
            container.Bind<CounterTextModelDataSO>().FromScriptableObject(_modelDataSO).AsTransient();
        }
    }
}
