using Batuhan.MVC.UnityComponents.Base;
using TimeCounter.Entities.CountIndicatorManager;
using UnityEngine;

namespace TimeCounter.Entities.CountIndicatorInstantiator
{
    internal class CountIndicatorInstantiatorView : BaseViewMonoBehaviour<ICountIndicatorInstantiatorContext>
    {
        [SerializeField]
        private Transform _indicatorParent;
        public Transform IndicatorParentTransform => _indicatorParent;
        public override void Dispose()
        {
        }
    }
}
