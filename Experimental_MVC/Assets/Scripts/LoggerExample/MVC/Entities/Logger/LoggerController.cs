using Assets.Scripts.Batuhan.Core.MVC.Base;
using System.Threading;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Logger
{
    internal class LoggerController : BaseController<LoggerModel, LoggerView>
    {
        public LoggerController(LoggerModel model, LoggerView view) : base(model, view)
        {
        }
    }
}
