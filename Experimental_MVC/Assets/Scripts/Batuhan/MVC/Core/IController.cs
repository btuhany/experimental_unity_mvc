
namespace Batuhan.MVC.Core
{
    public interface IController 
    {
        public IContext Context { get; }
    }

    public interface IController<TContext> where TContext : IContext
    {
        public TContext Context { get; }
    }
}
