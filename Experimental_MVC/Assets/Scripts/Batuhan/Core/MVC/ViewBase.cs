using UnityEngine;

namespace Batuhan.Core.MVC
{
    public abstract class ViewBase : MonoBehaviour, IUnityView
    {
        protected bool _isInitialized = false;
        public abstract bool IsInitialized { get; }
        public abstract IContext Context { get; }
        public abstract void Initialize(IContext context);
    }
}
