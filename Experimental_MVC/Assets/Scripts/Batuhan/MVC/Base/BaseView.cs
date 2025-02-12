using Batuhan.MVC.Core;

namespace Batuhan.MVC.Base
{
    public abstract class BaseView : IView, IContextHolder
    {
        public abstract IContext Context { get; }
    }
}
