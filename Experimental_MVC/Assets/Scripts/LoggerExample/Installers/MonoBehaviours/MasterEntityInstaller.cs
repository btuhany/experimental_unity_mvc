using Assets.Scripts.LoggerExample.Installers.ScriptableObjects;
using Assets.Scripts.LoggerExample.MVC.Entities.Circle;
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

        //TODOBY: Can be used for full app lifetime MVC Entities.
        //private const string ENTITY_INSTALLERS_PATH = "ScriptableObjects/MVC/EntityInstallers"; //TODOby: move to constants
        //private BaseEntityInstallerScriptableObject[] LoadEntityInstallers()
        //{
        //    var entityInstallers = Resources.LoadAll<BaseEntityInstallerScriptableObject>(ENTITY_INSTALLERS_PATH);
        //    return entityInstallers;
        //}
    }
}
