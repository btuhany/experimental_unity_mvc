using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.CountIndicator;
using UnityEngine;
using Zenject;

namespace TimeCounter.Installers
{
    [CreateAssetMenu(fileName = "CircleEntityInstaller", menuName = "Scriptable Objects/Batuhan/MVC/Installers/CircleEntityInstaller")]
    internal class CountIndicatorInstaller : BaseEntityInstallerSO
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
