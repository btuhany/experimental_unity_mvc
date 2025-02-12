using Batuhan.MVC.Core;

namespace TimeCounter.Entities.AppInitializer
{
    internal class AppInitializerModel : IModel
    {
        private float _appInitializationDelay = 1.0f;
        private IContext _context;
        public IContext Context => _context;

        public bool IsAppInitialized { get; }
        public float InitializationDelay { get => _appInitializationDelay; }
    }
}
