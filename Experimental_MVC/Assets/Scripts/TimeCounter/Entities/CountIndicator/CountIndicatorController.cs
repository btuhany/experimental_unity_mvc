using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace TimeCounter.Entities.CountIndicator
{
    internal class CountIndicatorController : BaseController<CountIndicatorModel, CountIndicatorView, ICountIndicatorContext>, ILifeCycleHandler
    {
        public class Factory : PlaceholderFactory<CountIndicatorController> { }

        [Inject]
        public CountIndicatorController(CountIndicatorModel model, CountIndicatorView view, ICountIndicatorContext context) : base(model, view, context)
        {
        }

        public void Initialize()
        {
            _view.Setup(_context);
            _model.Setup(_context);

            ChangeColorAfterSomeTime().Forget();
        }
        public void Dispose()
        {

        }
        private async UniTask ChangeColorAfterSomeTime()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(1, 10)));
            var randomColor = UnityEngine.Random.ColorHSV();
            randomColor.a = 1.0f;
        }
    }
}
