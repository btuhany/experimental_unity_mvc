using UnityEngine;

namespace Batuhan.Core.MVC
{
    public abstract class BaseControllerMonoBehaviour<TModel, TView> : MonoBehaviour, IMonoBehaviourController
    {
        private bool _isPreInitialized = false;
        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;

        protected bool _isInitialized = false;
        private IContext _context;

        protected TModel _model;
        protected TView _view;

        //TODO: Zenject?
        public void PreInitialize(TModel model, TView view)
        {
            if (!_isPreInitialized)
            {
                _model = model;
                _view = view;
                _isPreInitialized = true;
            }
        }
        public virtual void Initialize(IContext context)
        {
            if (!_isPreInitialized)
            {
                throw new System.Exception("PreInitialize method must be called before Initialize method");
            }

            if (!_isInitialized)
            {
                _context = context;
                _isInitialized = true;
            }
        }
    }
}
