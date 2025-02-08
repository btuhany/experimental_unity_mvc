using Assets.Scripts.Batuhan.Core.MVC.Base;
using System.Threading;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Logger
{
    internal class LoggerController : BaseController<CounterLoggerModel, CounterLoggerView>
    {
        public LoggerController(CounterLoggerModel model, CounterLoggerView view) : base(model, view)
        {
        }
    }
}
