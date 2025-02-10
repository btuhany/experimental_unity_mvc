using Assets.Scripts.LoggerExample.MVC.Entities.Circle;
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
            container.Bind<CircleView>().FromInstance(_circleViewPrefab).AsSingle();
            container.Bind<CircleContext>().AsTransient();
            container.BindFactory<CircleController, CircleController.Factory>().FromFactory<CircleFactory>();
        }
    }
}
