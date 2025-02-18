using System;

namespace Batuhan.MVC.Core
{
    public interface IModel : IDisposable
    {

    }
    public interface IModelContextual<TContext> : IModel, IRequiresContext<TContext>
        where TContext : IContext
    {

    }
}
