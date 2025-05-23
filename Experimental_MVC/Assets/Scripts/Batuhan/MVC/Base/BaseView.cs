﻿using Batuhan.MVC.Core;

namespace Batuhan.MVC.Base
{
    public abstract class BaseView<TContext> : IView, IRequiresContext<TContext>
        where TContext : IContext
    {
        protected TContext _context;
        public TContext Context => _context;

        public abstract void Dispose();

        public virtual void Setup(TContext context)
        {
            _context = context;
        }
    }
}
