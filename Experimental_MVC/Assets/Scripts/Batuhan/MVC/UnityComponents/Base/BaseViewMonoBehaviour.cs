using Batuhan.MVC.Core;
using UnityEngine;

namespace Batuhan.MVC.UnityComponents.Base
{
    //TODOby refactor model, view, controller classes immediately!
    public abstract class BaseViewMonoBehaviour : MonoBehaviour, IMonoBehaviourView
    {
        public abstract IContext Context { get; }
    }
}
