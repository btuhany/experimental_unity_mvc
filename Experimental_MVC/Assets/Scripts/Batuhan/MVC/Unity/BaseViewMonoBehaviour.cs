using UnityEngine;

namespace Batuhan.Core.MVC.Unity
{
    //TODOby refactor model, view, controller classes immediately!
    public abstract class BaseViewMonoBehaviour : MonoBehaviour, IMonoBehaviourView
    {
        public abstract IContext Context { get; }
    }
}
