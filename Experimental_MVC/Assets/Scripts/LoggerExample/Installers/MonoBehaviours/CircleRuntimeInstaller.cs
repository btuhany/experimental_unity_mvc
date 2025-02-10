using Assets.Scripts.LoggerExample.MVC.Entities.Circle;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers.MonoBehaviours
{
    public class CircleRuntimeInstaller : MonoInstaller
    {
        [SerializeField] private CircleView _circleViewPrefab;
        public override void InstallBindings()
        {
            Container.Bind<CircleView>().FromInstance(_circleViewPrefab).AsSingle();
            Container.Bind<CircleContext>().AsTransient();

            Container.BindFactory<CircleController, CircleController.Factory>()
                .FromFactory<CircleFactory>();
        }
    }
}
