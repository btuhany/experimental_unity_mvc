using Assets.Scripts.Batuhan.Core.MVC.Base;
using Assets.Scripts.LoggerExample.MVC.Entities.Counter;
using Batuhan.Core.MVC;
using Zenject;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Circle
{
    internal class CircleController : BaseController<CircleModel, CircleView>
    {
        public class Factory : PlaceholderFactory<CircleController> { }

        [Inject]
        public CircleController(CircleModel model, CircleView view, IContext context) : base(model, view, context)
        {
        }

        public override void Initialize()
        {
            _view.transform.position = UnityEngine.Random.insideUnitCircle * 7;
        }
    }
}
