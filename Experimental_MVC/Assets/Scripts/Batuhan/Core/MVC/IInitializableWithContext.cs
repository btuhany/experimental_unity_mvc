namespace Batuhan.Core.MVC
{
    //TODO: Better name? IContextHolder, IConcern...
    public interface IInitializableWithContext
    {
        public bool IsInitialized { get; }
        public IContext Context { get; }
        public void Initialize(IContext context);
    }
}
