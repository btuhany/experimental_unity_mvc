using UnityEngine;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    //We can use ScriptableObjectInstaller instead of this class,
    //but I want to make it seperate for the future, making it more modular and independent from Zenject
    public abstract class BaseEntityInstallerSO : ScriptableObject 
    {
        public abstract void InstallFrom(DiContainer container);
    }
}
