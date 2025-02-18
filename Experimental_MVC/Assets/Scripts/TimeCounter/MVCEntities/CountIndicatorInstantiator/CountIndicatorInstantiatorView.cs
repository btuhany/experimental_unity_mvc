using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using System;
using UnityEngine;

namespace TimeCounter.Entities.CountIndicatorInstantiator
{
    public interface ICountIndicatorInstantiatorView : IViewContextual<ICountIndicatorInstantiatorContext>
    {
        public Transform IndicatorParentTransform { get; }
    }
    internal class CountIndicatorInstantiatorView : BaseViewComponent, ICountIndicatorInstantiatorView
    {
        [SerializeField] private Transform _indicatorParent;
        private ICountIndicatorInstantiatorContext _context;
        public Transform IndicatorParentTransform => _indicatorParent;

        public override Type ContractTypeToBind => typeof(ICountIndicatorInstantiatorView);

        public ICountIndicatorInstantiatorContext Context => _context;

        public void Dispose()
        {
        }

        public void Setup(ICountIndicatorInstantiatorContext context)
        {
            _context = context;
        }
    }
}
