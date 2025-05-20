using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using UnityEngine;
using Zenject;

namespace ExperimentalMVC.SnakeExample.Entities.FoodController
{
    [CreateAssetMenu(fileName = "FoodInstaller", menuName = "Scriptable Objects/SnakeExample/Installers/FoodInstaller")]
    public class FoodControllerInstaller : BaseEntityInstallerSO
    {
        [SerializeField] private FoodView _foodView;
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<FoodView>().FromInstance(_foodView).AsTransient();
            container.Bind<ISceneLifeCycleManaged>().To<FoodController>().AsSingle();
        }
    }
}