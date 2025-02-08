
using Assets.Scripts.Batuhan.Core.MVC.Base;

namespace Assets.Scripts.LoggerExample.MVCEntities.CounterLogger
{
    internal class CounterLoggerController : BaseController<CounterLoggerModel, CounterLoggerView>
    {
        public CounterLoggerController(CounterLoggerModel model, CounterLoggerView view) : base(model, view)
        {
            
        }
    }
}
