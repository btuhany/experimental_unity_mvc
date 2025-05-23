using UnityEngine;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    //We can use ScriptableObjectInstaller instead of this class,
    //but I want to make it seperate for the future, making it more modular and independent from Zenject
    //[CreateAssetMenu(fileName = "BaseEntityInstallerSO", menuName = "Scriptable Objects/Batuhan/BaseEntityInstallerSO")]
    public abstract class BaseEntityInstallerSO : ScriptableObject 
    {
        public abstract void InstallFrom(DiContainer container);

        /// <summary>
        /// Custom initialization after zenject installer process
        /// </summary>
        public virtual void HandleOnAwake()
        {

        }
    }
}
