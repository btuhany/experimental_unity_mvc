using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using System.Collections.Generic;
using TimeCounter.Data;
using TimeCounter.Entities.CountIndicator;
using TimeCounter.Events.CoreEvents;
namespace TimeCounter.Entities.CountIndicatorInstantiator
{
    internal class CountIndicatorInstantiatorController : 
        BaseController<CountIndicatorInstantiatorModel, CountIndicatorInstantiatorView, ICountIndicatorInstantiatorContext>, ILifeCycleHandler
    {
        private CountIndicatorController.Factory _indicatorFactory;
        private List<CountIndicatorController> _indicatorRuntimeList;
        public CountIndicatorInstantiatorController(
            CountIndicatorController.Factory factory, 
            CountIndicatorInstantiatorModel model, 
            CountIndicatorInstantiatorView view,
            ICountIndicatorInstantiatorContext context) 
            : base(model, view, context)
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
            _indicatorRuntimeList.Clear();
        }
        private void OnTimeCountValueUpdated(TimeCountValueUpdatedEvent @event)
        {
            CountIndicatorCommonData commonData = new CountIndicatorCommonData();
            var index = _indicatorRuntimeList.Count;
            commonData.Index = index;

            var randomColor = UnityEngine.Random.ColorHSV();
            randomColor.a = 1.0f;
            commonData.Color = randomColor;

            commonData.Position = _model.CalcAndGetNextPosition();

            CountIndicatorInitData creationData = new CountIndicatorInitData();
            creationData.CommonData = commonData;
            creationData.ParentTransform = _view.IndicatorParentTransform;

            var indicator = _indicatorFactory.Create();
            indicator.Initialize(creationData);

            _indicatorRuntimeList.Add(indicator);
        }
    }
}
