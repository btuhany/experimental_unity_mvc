using Batuhan.Core.MVC.Base;
namespace Assets.Scripts.LoggerExample.MVC.Entities.Logger
{
    internal class LoggerMVCEntity : BaseEntity
        <LoggerModel, LoggerView, LoggerController>
    {
        public override void Initialize()
        {
            if (!_isInitialized)
            {
                var context = new LoggerContext();
                var model = new LoggerModel();
                var view = new LoggerView();
                var controller = new LoggerController(model, view);

                model.Initialize(context);
                view.Initialize(context);
                controller.Initialize(context);
            }

            _isInitialized = true;
        }
    }
}
