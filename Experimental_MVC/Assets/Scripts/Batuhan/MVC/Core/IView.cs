using System;

namespace Batuhan.MVC.Core
{
    public interface IView : IDisposable
    {
    }
    public interface IViewContextual<TContext> : IView, IRequiresContext<TContext>
        where TContext : IContext
    {

    }
}
