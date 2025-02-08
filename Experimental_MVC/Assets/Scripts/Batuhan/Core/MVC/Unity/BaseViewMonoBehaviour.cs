using UnityEngine;

namespace Batuhan.Core.MVC.Unity
{
    public abstract class BaseViewMonoBehaviour : MonoBehaviour, IUnityView
    {
        protected bool _isInitialized = false;
        public abstract bool IsInitialized { get; }
        public abstract IContext Context { get; }
        public abstract void Initialize(IContext context);
    }
}
