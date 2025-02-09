using Batuhan.Core.MVC.Base;
using Zenject;
namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterEntityInitializer : BaseEntityInitializer
        <CounterModel, CounterView, CounterController>
    {
        [Inject]
        public CounterEntityInitializer(CounterModel model, CounterView view, CounterController controller)
        {
            _model = model;
            _view = view;
            _controller = controller;
        }

        public override void Initialize()
        {
            if (!_isInitialized)
            {
                var context = new CounterContext();  //TODO_BY: bind context too

                _model.Initialize(context);           
                _view.Initialize(context);
                _controller.Initialize(context);
            }

            _isInitialized = true;
            //TODO MVC Entity Initialized and Ready to Use Event
        }
    }
}
