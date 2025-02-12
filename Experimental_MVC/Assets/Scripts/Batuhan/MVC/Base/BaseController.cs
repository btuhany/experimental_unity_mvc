using Batuhan.MVC.Core;
using NUnit.Framework;

namespace Batuhan.MVC.Base
{
    //TODOBY FIX
    public abstract class BaseControllerWithoutContext<TModel, TView> : IController
    {
        protected readonly TModel _model;
        protected readonly TView _view;

        protected bool _isInitialized = false;
        public bool IsInitialized => _isInitialized;
        public abstract IContext Context { get; }
        public abstract void Initialize();
        public BaseControllerWithoutContext(TModel model, TView view)
        {
            _model = model;
            _view = view;
        }
    }
        public abstract class BaseController<TModel, TView, TContext> : IController<TContext>
        where TModel : IModel 
        where TView : IView
        where TContext : IContext
    {
        protected readonly TModel _model;
        protected readonly TView _view;
        protected bool _isInitialized = false;
        public bool IsInitialized => _isInitialized;

        protected TContext _context;
        public TContext Context => _context;

        public abstract void Initialize();
        public BaseController(TModel model, TView view, TContext context)
        {
            _model = model;
            _view = view;
            _context = context;
        }
    }

    public abstract class BaseController<TModel, TContext> : IController<TContext>
    where TModel : IModel
    where TContext : IContext
    {
        protected readonly TModel _model;
        protected bool _isInitialized = false;
        public bool IsInitialized => _isInitialized;

        protected TContext _context;
        public TContext Context => _context;

        public abstract void Initialize();
        public BaseController(TModel model, TContext context)
        {
            _model = model;
            _context = context;
        }
    }
}
