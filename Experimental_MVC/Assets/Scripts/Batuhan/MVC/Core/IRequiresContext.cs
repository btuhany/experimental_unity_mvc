namespace Batuhan.MVC.Core
{
    public interface IRequiresContext<TContext> where TContext : IContext
    {
        public TContext Context { get; }
        public void Setup(TContext context);
    }
}
