using Assets.Scripts.Batuhan.Core.MVC.Base;
using Batuhan.Core.MVC;
using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterController : BaseController<CounterModel, CounterView>
    {
        [Inject]
        public CounterController(CounterModel model, CounterView view) : base(model, view)
        {
        }

        public override void Initialize(IContext context)
        {
            if (!_isInitialized)
            {
                base.Initialize(context);
                //TODO MVC Entity Initialized and Ready to Use Event

                _model.OnCountValueChanged += OnCountValueChanged;  //TODO Observer Pattern
                ActivateTick().Forget();
            }
        }
        private async UniTaskVoid ActivateTick()
        {
            while (true)
            {
                var secondsToWait = (float)(1f / _model.CountSpeed);
                await UniTask.Delay((int)(secondsToWait * 1000));
                _model.IncreaseCounter();
            }
        }
        private void OnDestroy()
        {
            _model.OnCountValueChanged -= OnCountValueChanged;
        }

        private IEnumerator CounterCoroutine()
        {
            while (true)
            {
                var secondsToWait = (float) (1f / _model.CountSpeed);
                yield return new WaitForSeconds(secondsToWait);
                _model.IncreaseCounter();
            }
        }


        private void OnCountValueChanged(int counterValue)
        {
            //TODO Command or Event Manager via Context
            _view.OnCountChanged(counterValue);
        }
    }
}
