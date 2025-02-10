using Assets.Scripts.Batuhan.Core.MVC.Base;
using Batuhan.Core.MVC;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Circle
{
    internal class CircleController : BaseController<CircleModel, CircleView>
    {
        public override IContext Context => _context;
        private ICircleContext _context;
        public class Factory : PlaceholderFactory<CircleController> { }

        [Inject]
        public CircleController(CircleModel model, CircleView view, ICircleContext context) : base(model, view)
        {
            _context = context;
        }

        public override void Initialize()
        {
            _view.transform.position = UnityEngine.Random.insideUnitCircle * 4;
            _view.Initialize(); //TODOBY change logic
            ChangeColorAfterSomeTime().Forget();
        }
        private async UniTask ChangeColorAfterSomeTime()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(1, 10)));
            var randomColor = UnityEngine.Random.ColorHSV();
            randomColor.a = 1.0f;
            _context.ChangeColorEvent?.Invoke();
            Debug.Log("Event Invoked!");
        }
    }
}
