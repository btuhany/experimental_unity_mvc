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
        public override void Initialize()
        {
            if (!_isInitialized)
            {
                var context = new CounterContext();
                var model = new CounterModel();

                model.Initialize(context);
                _view.Initialize(context);

                _controller.PreInitialize(model, _view);
                _controller.Initialize(context);
            }

            _isInitialized = true;

            //TODO MVC Entity Initialized and Ready to Use Event
        }
    }
}
