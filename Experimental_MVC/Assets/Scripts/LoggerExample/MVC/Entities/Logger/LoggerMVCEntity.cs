using Batuhan.Core.MVC.Base;
namespace Assets.Scripts.LoggerExample.MVC.Entities.Logger
{
    internal class LoggerMVCEntity : BaseEntity
        <CounterLoggerModel, CounterLoggerView, LoggerController>
    {
        public override void Initialize()
        {
            if (!_isInitialized)
            {
                var context = new CounterLoggerContext();
                var model = new CounterLoggerModel();
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
