using UnityEngine;
using Batuhan.MVC.UnityComponents.Base;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
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
                Container.Bind(viewType).FromInstance(view).AsTransient();
            }
        }
    }
}

