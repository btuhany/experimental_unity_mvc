using Assets.Scripts.Batuhan.Core.MVC.Base;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterController : BaseController<CounterModel, CounterView>
    {
        public CounterController(CounterModel model, CounterView view) : base(model, view)
        {
        }
    }
}
