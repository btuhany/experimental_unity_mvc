using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using System.Collections.Generic;
using TimeCounter.Data;
using TimeCounter.Entities.CountIndicator;
using TimeCounter.Events.CoreEvents;
namespace TimeCounter.Entities.CountIndicatorInstantiator
{
    internal class CountIndicatorInstantiatorController : 
        BaseController<ICountIndicatorInstantiatorModel, ICountIndicatorInstantiatorView, ICountIndicatorInstantiatorContext>, ILifeCycleHandler
    {
        private CountIndicatorController.Factory _indicatorFactory;
        private List<CountIndicatorController> _indicatorRuntimeList;
        public CountIndicatorInstantiatorController(
            CountIndicatorController.Factory factory,
            ICountIndicatorInstantiatorModel model,
            ICountIndicatorInstantiatorView view,
            ICountIndicatorInstantiatorContext context) 
            : base(model, view, context)
        {
            _indicatorRuntimeList = new();
            _indicatorFactory = factory;
        }
        public void OnAwakeCallback()
        {
            _context.EventBusCore.Subscribe<TickCountValueUpdatedEvent>(OnTimeCountValueUpdated);
        }
        public void OnDestroyCallback()
        {
            Dispose();
        }
        public override void Dispose()
        {
            _context.EventBusCore.Unsubscribe<TickCountValueUpdatedEvent>(OnTimeCountValueUpdated);
            DisposeAndClearIndicators();
            base.Dispose();
        }
        private void DisposeAndClearIndicators()
        {
            for (int i = 0; i < _indicatorRuntimeList.Count; i++)
            {
                _indicatorRuntimeList[i].Dispose();
            }
            _indicatorRuntimeList.Clear();
        }
        private void OnTimeCountValueUpdated(TickCountValueUpdatedEvent @event)
        {
            var tickCount = @event.UpdatedValue;
            var indicatorCount = _indicatorRuntimeList.Count;

            if (tickCount > indicatorCount)
            {
                var difference = tickCount - indicatorCount;
                for (int i = 0; i < difference; i++)
                {
                    CreateAndInitializeNewIndicator(indicatorCount + i);
                }
            }
            else if (tickCount < indicatorCount)
            {
                var difference = indicatorCount - tickCount;
                for (int i = 0; i < difference; i++)
                {
                    var lastIndicatorIndex = _indicatorRuntimeList.Count - 1;
                    _indicatorRuntimeList[lastIndicatorIndex].DestroyEntityForRuntime();
                    _indicatorRuntimeList.RemoveAt(lastIndicatorIndex);
                }
            }
        }
        private void CreateAndInitializeNewIndicator(int index)
        {
            CountIndicatorCommonData commonData = new CountIndicatorCommonData();
            commonData.Index = index;

            var randomColor = UnityEngine.Random.ColorHSV();
            randomColor.a = 1.0f;
            commonData.Color = randomColor;

            commonData.Position = _model.CalcPosition(index);

            CountIndicatorInitData creationData = new CountIndicatorInitData();
            creationData.CommonData = commonData;
            creationData.ParentTransform = _view.IndicatorParentTransform;

            var indicator = _indicatorFactory.Create();
            indicator.Initialize(creationData);

            _indicatorRuntimeList.Add(indicator);
        }
    }
}
