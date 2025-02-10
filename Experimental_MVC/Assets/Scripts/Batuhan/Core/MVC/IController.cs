namespace Batuhan.Core.MVC
{
    public interface IController : IInitializable
    {
        public IContext Context { get; }
    }
}
