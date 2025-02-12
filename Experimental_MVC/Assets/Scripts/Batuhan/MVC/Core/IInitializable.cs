namespace Batuhan.MVC.Core
{
    public interface IInitializable //TODOBY ZENJECT NAME CONFLICT
    {
        public bool IsInitialized { get; }
        public void Initialize();
    }
}
