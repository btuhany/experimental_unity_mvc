using Batuhan.Core.MVC;
namespace Assets.Scripts.Batuhan.Core.MVC.Base
{
    public abstract class BaseController<TModel, TView> : IController
    {
        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;

        private bool _isInitialized = false;
        private IContext _context;

        protected readonly TModel _model;
        protected readonly TView _view;
        public BaseController(TModel model, TView view)
        {
            _model = model;
            _view = view;
        }
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
