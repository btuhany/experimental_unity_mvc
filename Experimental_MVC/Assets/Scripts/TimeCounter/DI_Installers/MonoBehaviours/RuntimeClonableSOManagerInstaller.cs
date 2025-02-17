using Zenject;
using UnityEngine;
using Assets.Scripts.Batuhan.RuntimeCopyScriptableObjects;

namespace TimeCounter.Installers
{
    internal class RuntimeClonableSOManagerInstaller : MonoInstaller
    {
        [SerializeField] private RuntimeClonableSOManager _managerInstance;
        public override void InstallBindings()
        {
            Container.Bind<RuntimeClonableSOManager>().FromInstance(_managerInstance).AsSingle();
        }
    }
}
