using Batuhan.Core.MVC;

namespace Assets.Scripts.Batuhan.Core.MVC.Base
{
    public abstract class BaseModel : IModel
    {
        public IContext Context => _context;

        private IContext _context;


    }
}
