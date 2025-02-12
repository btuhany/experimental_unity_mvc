namespace Batuhan.MVC.Base
{
    public abstract class BaseController<TModel, TView> : IController
    {

        protected readonly TModel _model;
        protected readonly TView _view;
        protected bool _isInitialized = false;

        public bool IsInitialized => _isInitialized;

        public abstract IContext Context { get; }

        public abstract void Initialize();
        public BaseController(TModel model, TView view)
        {
            _model = model;
            _view = view;
        }
    }
}
