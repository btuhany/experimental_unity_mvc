using Batuhan.MVC.Core;
using System;
using UnityEngine;

namespace Batuhan.MVC.UnityComponents.Base
{
    public abstract class BaseViewComponent : MonoBehaviour 
    {
        public abstract Type ContractTypeToBind { get; }
    }
    public abstract class BaseViewMonoBehaviour<TContext> : BaseViewComponent, IViewContextual<TContext>
        where TContext : IContext
    {
        protected TContext _context;
        public TContext Context => _context;

        public virtual void Dispose()
        {
        }

        public virtual void Setup(TContext context)
        {
            _context = context;
        }
        public virtual void OnDestroy()
        {
            Dispose();
        }
    }
}
