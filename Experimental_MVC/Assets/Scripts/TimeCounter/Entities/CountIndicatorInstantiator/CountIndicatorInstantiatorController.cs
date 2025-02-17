using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using System.Collections.Generic;
using TimeCounter.Data;
using TimeCounter.Entities.CountIndicator;
using TimeCounter.Entities.CountIndicatorInstantiator;
using TimeCounter.Events.CoreEvents;
namespace TimeCounter.Entities.CountIndicatorManager
{
    internal class CountIndicatorInstantiatorController : 
        BaseControllerWithoutModel<CountIndicatorInstantiatorView, ICountIndicatorInstantiatorContext>, ILifeCycleHandler
    {
        private CountIndicatorController.Factory _indicatorFactory;
        private List<CountIndicatorController> _indicatorRuntimeList;
        public CountIndicatorInstantiatorController(CountIndicatorController.Factory factory, CountIndicatorInstantiatorView view,
            ICountIndicatorInstantiatorContext context) 
            : base(view, context)
        {
            _indicatorRuntimeList = new();
            _indicatorFactory = factory;
        }
        public void Initialize()
        {
            _context.EventBusCore.Subscribe<TimeCountValueUpdatedEvent>(OnTimeCountValueUpdated);
        }
        public void Dispose()
        {
            _context.EventBusCore.Unsubscribe<TimeCountValueUpdatedEvent>(OnTimeCountValueUpdated);
            CleanUpIndicators();
        }
        private void CleanUpIndicators()
        {
            for (int i = 0; i < _indicatorRuntimeList.Count; i++)
            {
                _indicatorRuntimeList[i].Dispose();
            }
        }
        private void OnTimeCountValueUpdated(TimeCountValueUpdatedEvent @event)
        {
            CountIndicatorCommonData commonData = new CountIndicatorCommonData();
            var indice = _indicatorRuntimeList.Count;
            commonData.Indice = indice;

            var randomColor = UnityEngine.Random.ColorHSV();
            randomColor.a = 1.0f;
            commonData.Color = randomColor;

            //TODOBY Increase radius after a circle has been completed.
            var radius = 4.0f;
            var posVec2 = new UnityEngine.Vector2(
                UnityEngine.Mathf.Cos(indice + 2.0f), 
                UnityEngine.Mathf.Sin(indice + 2.0f)) * radius;
            commonData.Position = new UnityEngine.Vector3(posVec2.x, posVec2.y, 1.0f);

            CountIndicatorInitData creationData = new CountIndicatorInitData();
            creationData.CommonData = commonData;
            creationData.ParentTransform = _view.IndicatorParentTransform;

            var indicator = _indicatorFactory.Create();
            indicator.Initialize(creationData);

            _indicatorRuntimeList.Add(indicator);
        }
    }
}
