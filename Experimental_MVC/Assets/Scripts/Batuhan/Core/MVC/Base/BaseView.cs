using Batuhan.Core.MVC;

namespace Assets.Scripts.Batuhan.Core.MVC.Base
{
    public abstract class BaseView : IView
    {
        public bool IsInitialized => _isInitialized;

        public IContext Context => _context;

        private bool _isInitialized = false;
        private IContext _context;
        public virtual void Initialize(IContext context)
        {
            if (!_isInitialized)
            {
                _context = context;
                _isInitialized = true;
            }
        }
    }
}
