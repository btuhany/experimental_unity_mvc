using Assets.Scripts.LoggerExample.Installers.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers.MonoBehaviours
{
    internal class MasterEntityInstaller : MonoInstaller
    {
        [SerializeField] private BaseEntityInstallerScriptableObject[] _entityInstallerScriptableObjects;
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
