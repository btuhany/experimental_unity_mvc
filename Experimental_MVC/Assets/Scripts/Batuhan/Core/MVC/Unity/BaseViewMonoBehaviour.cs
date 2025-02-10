using UnityEngine;

namespace Batuhan.Core.MVC.Unity
{
    //TODOby refactor model, view, controller classes immediately!
    public abstract class BaseViewMonoBehaviour : MonoBehaviour, IMonoBehaviourView
    {
        public IContext Context => _context;
        private IContext _context;
    }
}
