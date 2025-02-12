using Assets.Scripts.LoggerExample.MVC.Entities.Circle;
using Assets.Scripts.LoggerExample.MVC.Entities.Counter;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CircleEntityInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CircleEntityInstaller")]
    internal class CountIndicatorInstaller : BaseEntityInstallerScriptableObject
    {
        [SerializeField] private CountIndicatorView _circleViewPrefab;
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<ICountIndicatorContext>().To<CountIndicatorContext>().AsTransient();
            container.Bind<CountIndicatorModel>().AsTransient();
            container.Bind<CountIndicatorView>().FromInstance(_circleViewPrefab).AsSingle();
            container.BindFactory<CountIndicatorController, CountIndicatorController.Factory>().FromFactory<CountIndicatorFactory>();
        }
    }
}
