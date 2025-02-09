using Assets.Scripts.LoggerExample.MVC.Entities.Counter;
using Batuhan.Core.MVC.Unity;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.Installers
{
    public class MasterViewInstaller : MonoInstaller
    {
        [SerializeField] private BaseViewMonoBehaviour[] _views;
        public override void InstallBindings()
        {
            for (int i = 0; i < _views.Length; i++)
            {
                var view = _views[i];
                var viewType = view.GetType();
                Container.Bind(viewType).FromInstance(view).AsSingle();
            }
            CounterEntityInstaller.Install(Container);
        }
    }
}

