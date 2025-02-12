using UnityEngine;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    internal class MasterScriptableObjectInstaller : MonoInstaller
    {
        [SerializeField] private BaseEntityInstallerSO[] _entityInstallerScriptableObjects;
        public override void InstallBindings()
        {
            var entityInstallers = _entityInstallerScriptableObjects;
            foreach (var installer in entityInstallers)
            {
                installer.InstallFrom(Container);
            }
        }
    }
}
