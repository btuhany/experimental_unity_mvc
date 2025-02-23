using Batuhan.MVC.Core;
using System;

namespace Batuhan.MVC.Base
{
    public abstract class BaseController<TModel, TView, TContext> : IController, IDisposable
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

        public virtual void Dispose()
        {
            _model.Dispose();
            _view.Dispose();
            _context.Dispose();
        }
    }
    public abstract class BaseControllerWithModelAndContext<TModel, TContext> : IController, IDisposable
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
        public virtual void Dispose()
        {
            _model.Dispose();
            _context.Dispose();
        }
    }
    public abstract class BaseControllerWithViewAndContext<TView, TContext> : IController, IDisposable
        where TView : IView
        where TContext : IContext
    {
        protected readonly TView _view;
        protected readonly TContext _context;

        public BaseControllerWithViewAndContext(TView view, TContext context)
        {
            _view = view;
            _context = context;
        }
        public virtual void Dispose()
        {
            _view.Dispose();
            _context.Dispose();
        }
    }
    public abstract class BaseControllerWithViewOnly<TView> : IController, IDisposable
      where TView : IView
    {
        protected readonly TView _viewModel;

        public BaseControllerWithViewOnly(TView view)
        {
            _viewModel = view;
        }
        public virtual void Dispose()
        {
            _viewModel.Dispose();
        }
    }
    public abstract class BaseController<TContext> : IController, IDisposable
        where TContext : IContext
    {
        protected readonly TContext _context;
        public BaseController(TContext context)
        {
            _context = context;
        }
        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}
