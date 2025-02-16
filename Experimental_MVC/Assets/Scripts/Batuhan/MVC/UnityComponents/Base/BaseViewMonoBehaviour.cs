using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Core;
using System;
using UnityEngine;

namespace Batuhan.MVC.UnityComponents.Base
{
    //TODOby refactor model, view, controller classes immediately!
    public abstract class BaseViewMonoBehaviour : MonoBehaviour, IMonoBehaviourView, IDisposable
    {
        public abstract IContext Context { get; }

        public abstract void Dispose();
    }
}
