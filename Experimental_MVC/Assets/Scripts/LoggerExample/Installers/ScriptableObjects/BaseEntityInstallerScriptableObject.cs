using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers.ScriptableObjects
{
    //We can use ScriptableObjectInstaller instead of this class,
    //but I want to make it seperate for the future, making it more modular and independent from Zenject

    //[CreateAssetMenu(fileName = "BaseEntityInstallerScriptableObject", menuName = "Scriptable Objects/Batuhan/MVC/Installers/BaseEntityInstallerScriptableObject")]
    public abstract class BaseEntityInstallerScriptableObject : ScriptableObject 
    {
        public abstract void InstallFrom(DiContainer container);
    }
}
