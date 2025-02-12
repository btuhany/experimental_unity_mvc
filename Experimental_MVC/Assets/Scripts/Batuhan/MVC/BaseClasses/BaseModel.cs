using Batuhan.MVC.Core;

namespace Batuhan.MVC.Base
{
    public abstract class BaseModel : IModel
    {
        public IContext Context => _context;

        private IContext _context;


    }
}
