using Assets.Scripts.LoggerExample.MVC.Entities.Circle;
using Assets.Scripts.LoggerExample.MVC.Entities.Counter;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CircleEntityInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CircleEntityInstaller")]
    internal class CircleEntityInstaller : BaseEntityInstallerScriptableObject
    {
        [SerializeField] private CircleView _circleViewPrefab;
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ICircleContext>().To<CircleContext>().AsTransient();
            container.Bind<CircleModel>().AsTransient();
            container.Bind<CircleView>().FromInstance(_circleViewPrefab).AsSingle();
            container.BindFactory<CircleController, CircleController.Factory>().FromFactory<CircleFactory>();
        }
    }
}
