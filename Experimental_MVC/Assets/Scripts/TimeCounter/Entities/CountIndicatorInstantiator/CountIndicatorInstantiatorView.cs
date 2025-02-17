using Batuhan.MVC.UnityComponents.Base;
using UnityEngine;

namespace TimeCounter.Entities.CountIndicatorInstantiator
{
    internal class CountIndicatorInstantiatorView : BaseViewMonoBehaviour<ICountIndicatorInstantiatorContext>
    {
        [SerializeField]
        private Transform _indicatorParent;
        public Transform IndicatorParentTransform => _indicatorParent;
    }
}
