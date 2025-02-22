using Batuhan.MVC.Core;
using System;
using UnityEngine;

namespace Batuhan.MVC.UnityComponents.Base
{
    public abstract class BaseViewMonoBehaviour : MonoBehaviour, IView
    {
        public abstract Type ContractTypeToBind { get; }

        public virtual void Dispose() 
        {
            Destroy(gameObject);
        }
    }
}
