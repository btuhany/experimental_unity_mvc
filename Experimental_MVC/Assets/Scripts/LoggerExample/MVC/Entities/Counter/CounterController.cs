using Batuhan.Core.MVC;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterController : BaseControllerMonoBehaviour<CounterModel, CounterView>
    {
        public override void Initialize(IContext context)
        {
            if (!_isInitialized)
            {
                base.Initialize(context);
                //TODO MVC Entity Initialized and Ready to Use Event

                _model.OnCountValueChanged += OnCountValueChanged;  //TODO Observer Pattern

                StartCoroutine(CounterCoroutine());
            }
        }

        private void OnDestroy()
        {
            _model.OnCountValueChanged -= OnCountValueChanged;
        }

        private IEnumerator CounterCoroutine()
        {
            while(true)
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
