using UnityEngine;
using Batuhan.MVC.UnityComponents.Base;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    //Used install views that EXIST IN THE SAME SCENE WITH SCENE CONTEXT
    public class MasterViewMonoBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private BaseViewMonoBehaviour[] _views;
        public override void InstallBindings()
        {
            for (int i = 0; i < _views.Length; i++)
            {
                var view = _views[i];
                var viewType = view.ContractTypeToBind;
                Container.Bind(viewType).FromInstance(view).AsTransient();
            }
        }
    }
}

