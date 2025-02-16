using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace TimeCounter.Entities.CountIndicator
{
    internal class CountIndicatorController : BaseController<CountIndicatorModel, CountIndicatorView, ICountIndicatorContext>
    {
        public class Factory : PlaceholderFactory<CountIndicatorController> { }

        [Inject]
        public CountIndicatorController(CountIndicatorModel model, CountIndicatorView view, ICountIndicatorContext context) : base(model, view, context)
        {
            UnityEngine.Debug.Log("Instantiated new CountIndicatorController");
        }

        public void Initialize()
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
        }
    }
}
