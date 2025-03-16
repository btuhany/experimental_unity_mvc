using Batuhan.MVC.UnityComponents.Zenject;
using Batuhan.RuntimeCopyScriptableObjects;
using SnakeExample.Config;
using UnityEngine;
using Zenject;

namespace SnakeExample.Installers
{
    [CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Scriptable Objects/SnakeExample/Installers/GameConfigInstaller")]
    internal class ConfigInstaller : BaseEntityInstallerSO
    {
        [SerializeField] private GameConfigDataSO _dataSO;
        public override void InstallFrom(DiContainer container)
        {
            container.Bind<GameConfigDataSO>().FromScriptableObject(_dataSO).AsTransient();
        }
    }
}
