using UnityEngine;

namespace Batuhan.Core.MVC.Unity
{
    public abstract class BaseViewMonoBehaviour : MonoBehaviour, IMonoBehaviourView
    {
        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;

        protected bool _isInitialized = false;
        private IContext _context;
        public virtual void Initialize(IContext context)
        {
            if (!_isInitialized)
            {
                _context = context;
                _isInitialized = true;
            }
        }
    }
}
