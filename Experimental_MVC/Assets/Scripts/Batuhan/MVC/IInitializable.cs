namespace Batuhan.Core.MVC
{
    public interface IInitializable
    {
        public bool IsInitialized { get; }
        public void Initialize();
    }
}
