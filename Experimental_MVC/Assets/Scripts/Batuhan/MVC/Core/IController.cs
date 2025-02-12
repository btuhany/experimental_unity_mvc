namespace Batuhan.MVC.Core
{
    public interface IController : IInitializable
    {
        public IContext Context { get; }
    }
}
