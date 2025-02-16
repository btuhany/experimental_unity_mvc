using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using TimeCounter.Entities.CountIndicator;
using Zenject;

namespace TimeCounter.Entities.CountIndicatorManager
{
    public class CountIndicatorInstantiatorController : BaseController<ICountIndicatorInstantiatorContext>, ILifeCycleHandler
    {
        [Inject]
        CountIndicatorController.Factory _indicatorFactory;
        public CountIndicatorInstantiatorController(ICountIndicatorInstantiatorContext context) : base(context)
        {
        }

        public void Dispose()
        {
        }

        public void Initialize()
        {
        }
    }
}
