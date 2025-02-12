namespace Batuhan.MVC.Core
{
    public interface IInitializable
    {
        public bool IsInitialized { get; }
        public void Initialize();
    }
}
