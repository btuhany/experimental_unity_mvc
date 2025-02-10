using Assets.Scripts.LoggerExample.Installers.ScriptableObjects;
using Batuhan.Core.MVC.Unity;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers.MonoBehaviours
{
    internal class MasterEntityInstaller : MonoInstaller
    {
        private const string ENTITY_INSTALLERS_PATH = "ScriptableObjects/MVC/EntityInstallers"; //TODOby: move to constants
        public override void InstallBindings()
        {
            var entityInstallers = LoadEntityInstallers();
            foreach (var installer in entityInstallers)
            {
                installer.InstallFrom(Container);
            }
        }

        private BaseEntityInstallerScriptableObject[] LoadEntityInstallers()
        {
            var entityInstallers = Resources.LoadAll<BaseEntityInstallerScriptableObject>(ENTITY_INSTALLERS_PATH);
            return entityInstallers;
        }
    }
}
