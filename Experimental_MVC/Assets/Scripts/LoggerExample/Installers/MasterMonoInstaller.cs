using Assets.Scripts.LoggerExample.MVC.Entities.Counter;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers
{
    public class MasterMonoInstaller : MonoInstaller
    {
        //TODOby: Composite Installer can work
        [SerializeField] private CounterView _counterView;
        public override void InstallBindings()
        {
            Container.Bind<CounterView>().FromInstance(_counterView).AsSingle();
            CounterEntityInstaller.Install(Container);
        }
    }
}

