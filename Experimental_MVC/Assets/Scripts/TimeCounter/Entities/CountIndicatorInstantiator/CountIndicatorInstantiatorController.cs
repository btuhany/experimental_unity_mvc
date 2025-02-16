using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using TimeCounter.Data;
using TimeCounter.Entities.CountIndicator;
using TimeCounter.Entities.CountIndicatorInstantiator;
using TimeCounter.Events.CoreEvents;
using UnityEditor.Rendering.LookDev;

namespace TimeCounter.Entities.CountIndicatorManager
{
    internal class CountIndicatorInstantiatorController : 
        BaseControllerWithoutModel<CountIndicatorInstantiatorView, ICountIndicatorInstantiatorContext>, ILifeCycleHandler
    {
        CountIndicatorController.Factory _indicatorFactory;
        public CountIndicatorInstantiatorController(CountIndicatorController.Factory factory, CountIndicatorInstantiatorView view,
            ICountIndicatorInstantiatorContext context) 
            : base(view, context)
        {
            _indicatorFactory = factory;
        }
        public void Initialize()
        {
            _context.EventBusCore.Subscribe<TimeCountValueUpdatedEvent>(OnTimeCountValueUpdated);
        }
        public void Dispose()
        {
            _context.EventBusCore.Unsubscribe<TimeCountValueUpdatedEvent>(OnTimeCountValueUpdated);
        }
        private void OnTimeCountValueUpdated(TimeCountValueUpdatedEvent @event)
        {
            CountIndicatorCommonData commonData = new CountIndicatorCommonData();
            commonData.Indice = UnityEngine.Random.Range(0, 100);
            var randomColor = UnityEngine.Random.ColorHSV();
            randomColor.a = 1.0f;
            commonData.Color = randomColor;
            var randomVec2 = UnityEngine.Random.insideUnitCircle * 5.0f;
            commonData.Position = new UnityEngine.Vector3(randomVec2.x, randomVec2.y, 1.0f);

            CountIndicatorInitData creationData = new CountIndicatorInitData();
            creationData.CommonData = commonData;
            creationData.ParentTransform = _view.IndicatorParentTransform;

            var indicator = _indicatorFactory.Create();
            indicator.Initialize(creationData);
        }
    }
}
