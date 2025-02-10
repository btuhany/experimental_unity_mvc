using Batuhan.Core.MVC;
namespace Assets.Scripts.Batuhan.Core.MVC.Base
{
    public abstract class BaseController<TModel, TView> : IController
    {
        private IContext _context;

        protected readonly TModel _model;
        protected readonly TView _view;
        protected bool _isInitialized = false;

        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;

        public abstract void Initialize();
        public BaseController(TModel model, TView view, IContext context)
        {
            _model = model;
            _view = view;
            _context = context;
        }
    }
}
