using Batuhan.Core.MVC.Base;
namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterMVCEntity : BaseEntity
        <CounterModel, CounterView, CounterController>
    {
        public CounterMVCEntity(CounterController controller, CounterView view)
        {
            _view = view;
            _controller = controller;
        }
        public CounterMVCEntity(CounterView view)
        {
            _view = view;
        }
        public override void Initialize()
        {
            if (!_isInitialized)
            {
               
                var context = new CounterContext();
                var model = new CounterModel();

                if (_controller is null)
                    _controller = new CounterController(model, _view);


                model.Initialize(context);           
                _view.Initialize(context);
                _controller.Initialize(context);
            }

            _isInitialized = true;

            //TODO MVC Entity Initialized and Ready to Use Event
        }
    }
}
