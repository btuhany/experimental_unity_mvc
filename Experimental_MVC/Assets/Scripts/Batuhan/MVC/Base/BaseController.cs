using Batuhan.MVC.Core;

namespace Batuhan.MVC.Base
{
    public abstract class BaseController<TModel, TView, TContext> : IController
        where TModel : IModel
        where TView : IView
        where TContext : IContext
    {
        protected readonly TModel _model;
        protected readonly TView _view;
        protected readonly TContext _context;

        public BaseController(TModel model, TView view, TContext context)
        {
            _model = model;
            _view = view;
            _context = context;
        }
    }
    public abstract class BaseControllerWithModelAndContext<TModel, TContext> : IController
        where TModel : IModel
        where TContext : IContext
    {
        protected readonly TModel _model;
        protected readonly TContext _context;

        public BaseControllerWithModelAndContext(TModel model, TContext context)
        {
            _model = model;
            _context = context;
        }
    }
    public abstract class BaseControllerWithViewAndContext<TView, TContext> : IController
        where TView : IView
        where TContext : IContext
    {
        protected readonly TView _model;
        protected readonly TContext _context;

        public BaseControllerWithViewAndContext(TView model, TContext context)
        {
            _model = model;
            _context = context;
        }
    }
    public abstract class BaseController<TContext> : IController
        where TContext : IContext
    {
        protected readonly TContext _context;
        public BaseController(TContext context)
        {
            _context = context;
        }
    }
}
