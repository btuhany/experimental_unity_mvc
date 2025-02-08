using Batuhan.Core.MVC.Base;
namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterMVCEntity : BaseEntity
        <ControllerModel, CounterLoggerView, CounterController>
    {
        public override void Initialize()
        {
            if (!_isInitialized)
            {
                var context = new CounterContext();
                var model = new ControllerModel();
                var view = new CounterLoggerView();
                var controller = new CounterLoggerController(model, view);

                model.Initialize(context);
                view.Initialize(context);
                controller.Initialize(context);
            }

            _isInitialized = true;
        }
    }
}
