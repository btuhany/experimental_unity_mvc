using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using TimeCounter.Entities.SceneChanger;
using UnityEngine;
using Zenject;

namespace TimeCounter.Installers
{
    [CreateAssetMenu(fileName = "SceneChangerInstaller", menuName = "Scriptable Objects/App/Installers/SceneChangerInstaller")]
    public class SceneChangerInstaller : BaseEntityInstallerSO
    {
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<IAppLifeCycleManaged>().To<SceneChangerController>().AsSingle();
        }
    }
}
